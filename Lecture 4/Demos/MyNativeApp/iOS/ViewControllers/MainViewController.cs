using System;

namespace MyNativeApp.iOS
{
	public partial class MainViewController : IoCAwareViewController<MainViewModel>
	{
		public MainViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.Bind(nameof(MainViewModel.CanLogin), () =>
				{
					this.LoginButton.Enabled = this.ViewModel.CanLogin;
				});
		}

		protected override void BindEvents()
		{
			base.BindEvents();

			this.EmailTextField.EditingChanged += this.OnEmailChanged;
			this.LoginButton.TouchUpInside += this.OnLoginTouchUpInside;
		}

		private void OnLoginTouchUpInside (object sender, EventArgs e)
		{
			this.ViewModel.Login();
		}

		protected override void UnbindEvents()
		{
			base.UnbindEvents();

			this.EmailTextField.EditingChanged -= this.OnEmailChanged;
		}

		private void OnEmailChanged (object sender, EventArgs e)
		{
			this.ViewModel.UserEmail = this.EmailTextField.Text;
		}
	}
}