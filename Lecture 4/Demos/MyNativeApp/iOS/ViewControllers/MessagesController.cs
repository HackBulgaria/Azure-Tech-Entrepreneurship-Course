using System;
using UIKit;
using Foundation;
using System.Linq;

namespace MyNativeApp.iOS
{
	public partial class MessagesController : IoCAwareViewController<MessagesViewModel>
	{
		public MessagesController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TableView.WeakDelegate = this;
			this.TableView.WeakDataSource = this;

			this.ViewModel.ReloadMessages();

			this.Bind(nameof(MessagesViewModel.Messages), () =>
				{
					this.TableView.ReloadData();
				});

			this.Bind(nameof(MessagesViewModel.CanReloadMessages), () =>
				{
					this.RefreshButton.Enabled = this.ViewModel.CanReloadMessages;
				});

			this.Bind(nameof(MessagesViewModel.CanSendMessage), () =>
				{
					this.SendButton.Enabled = this.ViewModel.CanSendMessage;
				});

			this.Bind(nameof(MessagesViewModel.Message), () =>
				{
					this.NewMessageTextField.Text = this.ViewModel.Message;
				});
		}

		protected override void BindEvents()
		{
			base.BindEvents();

			this.RefreshButton.Clicked += this.OnRefreshClicked;
			this.NewMessageTextField.EditingChanged += this.OnMessageChanged;
			this.SendButton.TouchUpInside += this.SendMessage;
		}


		protected override void UnbindEvents()
		{
			base.UnbindEvents();

			this.RefreshButton.Clicked -= this.OnRefreshClicked;
			this.NewMessageTextField.EditingChanged -= this.OnMessageChanged;
			this.SendButton.TouchUpInside -= this.SendMessage;
		}

		private void OnMessageChanged (object sender, EventArgs e)
		{
			this.ViewModel.Message = this.NewMessageTextField.Text;
		}

		private void OnRefreshClicked (object sender, EventArgs e)
		{
			this.ViewModel.ReloadMessages();
		}

		private void SendMessage (object sender, EventArgs e)
		{
			this.ViewModel.SendMessage();
		}

		#region TableViewImplemenation

		[Export ("numberOfSectionsInTableView:")]
		public nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		[Export ("tableView:numberOfRowsInSection:")]
		public nint RowsInSection (UITableView tableview, nint section)
		{
			return this.ViewModel.Messages.Count();
		}

		[Export ("tableView:heightForRowAtIndexPath:")]
		public nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 60;
		}

		[Export ("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("channelInfo");
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, "channelInfo");
			}
			var message = this.ViewModel.Messages.ElementAt(indexPath.Row);
			cell.TextLabel.Text = string.Format("{0}: {1}", message.User, message.Text);
			if (message.IsCurrentUser)
			{
				cell.TextLabel.TextColor = UIColor.Red;
			}
			cell.DetailTextLabel.Text = message.Date.ToString("HH:mm dd-MM-yyyy");

			return cell;
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{

		}

		#endregion
	}
}