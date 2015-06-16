using System;
using UIKit;
using System.Collections.Generic;
using Foundation;
using BraintreeBindings;
using BigTed;
using UserVoiceBindingsClassic;
using Xamarin;
using Facebook.LoginKit;
using CoreGraphics;

namespace HelloPayments
{
	public partial class ViewController : UIViewController
	{
		private LoginButton loginView;

		private string UserEmail
		{
			get
			{
				return string.IsNullOrEmpty(this.EmailInputField.Text) ? this.EmailInputField.Placeholder : this.EmailInputField.Text;
			}
		}

		private readonly SimpleBraintreeDelegate braintreeDelegate;

		public ViewController(IntPtr handle) : base(handle)
		{
			this.braintreeDelegate = new SimpleBraintreeDelegate(this.PaymentSuccess, this.PaymentFailure);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.loginView = new LoginButton(new CGRect(10, 10, 300, 40)) {
				ReadPermissions = new[] { "email" }	
			};
			this.loginView.Completed += (sender, e) =>
			{
				if (e.Error == null)
				{
					var request = new Facebook.CoreKit.GraphRequest("me", null);
						request.Start((con, result, error) =>
							{
								if (error == null)
								{
									this.EmailInputField.Text = ((NSDictionary)result).ValueForKeyPath(new NSString("email")).ToString();
								}
							});
				}
			};

			this.Add(this.loginView);

			this.BuyButton.TouchUpInside += async (sender, e) => 
				{
					LeanplumBindings.Leanplum.SetUserId(this.UserEmail);
					LeanplumBindings.Leanplum.Track("Purchase intent", 10);
					Insights.Identify(this.UserEmail, "a", "b");
					TaperecorderBinding.TapeRecorder.SetUserID(this.UserEmail);

					try
					{
						this.BuyButton.Enabled = false;

						var endpoint = string.Format("{0}/payment/token", Constants.ServerApiAddress);
						var token = await HttpRequestHelper.Post<string>(endpoint, this.UserEmail);

						var braintree = Braintree.Create(token);
						var btVC = braintree.CreateDropInViewController(this.braintreeDelegate);
						btVC.FetchPaymentMethods();

						btVC.SummaryTitle = "New subscription";
						btVC.SummaryDescription = "New subscription for June for everything!";
						btVC.DisplayAmount = "$10.00";
						btVC.CallToActionText = "Get it now!";

						var nav = new UINavigationController(btVC);

						btVC.NavigationItem.LeftBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, (_, __) =>
							{
								this.PaymentFailure();
							});
						this.PresentViewController(nav, true, null);
					}
					catch (Exception ex)
					{
						Console.WriteLine (ex.Message);
					}
					finally
					{
						this.BuyButton.Enabled = true;
					}
				};
		}

		private async void PaymentSuccess(string nonce)
		{
			try
			{
				int? i = null;
				Console.WriteLine (i.Value.ToString());
			}
			catch (Exception ex)
			{
				Insights.Report(ex);
			}


			BTProgressHUD.Show("Contacting server!");
			this.PresentedViewController.DismissViewController(true, null);

			var endpoint = string.Format("{0}/payment/purchase", Constants.ServerApiAddress);
			var payload = new PaymentRequest 
				{
					Amount = 1,
					Price = 10,
					Email = this.UserEmail,
					Nonce = nonce
				};

			var response = await HttpRequestHelper.Post<PaymentResponse>(endpoint, payload);
			if (response.IsSuccess)
			{
				BTProgressHUD.ShowSuccessWithStatus("Payment success!");
				LeanplumBindings.Leanplum.Track("Purchase success", 10);
			}
			else
			{
				BTProgressHUD.ShowErrorWithStatus("Oh noez!");
			}
		}

		private void PaymentFailure()
		{
			this.PresentedViewController.DismissViewController(true, null);

			BTProgressHUD.ShowErrorWithStatus("Canceled!");
			LeanplumBindings.Leanplum.Track("Purchase canceled", 10);
		}

		partial void ShowFeedback(NSObject sender)
		{
			var config = UVConfig.ConfigWithSite("techent.uservoice.com");
			config.IdentifyUser(this.UserEmail, this.UserEmail, this.UserEmail);
			UserVoice.Initialize(config);
			UserVoice.PresentUserVoiceInterface(this);
		}

		private class SimpleBraintreeDelegate : BTDropInViewControllerDelegate
		{
			private readonly Action<string> onSuccess;
			private readonly Action onCanceled;

			public SimpleBraintreeDelegate (Action<string> onSuccess, Action onCanceled)
			{
				this.onSuccess = onSuccess;
				this.onCanceled = onCanceled;
			}

			public override void OnCanceled (BTDropInViewController controller)
			{
				this.onCanceled();
			}

			public override void OnSuccess (BTDropInViewController controller, BTPaymentMethod paymentMethod)
			{
				this.onSuccess(paymentMethod.Nonce);
			}
		}
	}

	public class PaymentRequest
	{
		public string Nonce { get; set; }

		public string Email { get; set; }

		public int Amount { get; set; }

		public decimal Price { get; set; }
	}

	public class PaymentResponse
	{
		public bool IsSuccess { get; set; }

		public IEnumerable<string> Errors { get; set; }
	}
}