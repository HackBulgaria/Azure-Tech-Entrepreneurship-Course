using System;
using UIKit;
using Foundation;
using System.Linq;

namespace MyNativeApp.iOS
{
	public partial class ChannelViewController : IoCAwareViewController<ChannelViewModel>
	{
		[Import]
		public Lazy<INavigationService> NavigationService { get; set; }

		public ChannelViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.ViewModel.ReloadChannels();

			this.Bind(nameof(ChannelViewModel.Channels), () =>
				{
					this.TableView.ReloadData();
				});

			this.Bind(nameof(ChannelViewModel.CanReloadChannels), () =>
				{
					this.ReloadChannelsButton.Enabled = this.ViewModel.CanReloadChannels;
				});

			this.Bind(nameof(ChannelViewModel.CanCreateChannel), () =>
				{
					this.AddChannelButton.Enabled = this.ViewModel.CanCreateChannel;
				});

			this.TableView.WeakDelegate = this;
			this.TableView.WeakDataSource = this;
		}

		protected override void BindEvents()
		{
			base.BindEvents();

			this.AddChannelButton.Clicked += this.OnAddChannelClicked;
			this.ReloadChannelsButton.Clicked += this.OnReloadChannelsClicked;
		}

		protected override void UnbindEvents()
		{
			base.UnbindEvents();

			this.AddChannelButton.Clicked -= this.OnAddChannelClicked;
			this.ReloadChannelsButton.Clicked -= this.OnReloadChannelsClicked;
		}

		private void OnAddChannelClicked (object sender, EventArgs e)
		{
			this.ViewModel.CreateChannel();
		}

		private void OnReloadChannelsClicked (object sender, EventArgs e)
		{
			this.ViewModel.ReloadChannels();
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
			return this.ViewModel.Channels.Count();
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
				cell = new UITableViewCell(UITableViewCellStyle.Default, "channelInfo");
			}
			cell.TextLabel.Text = this.ViewModel.Channels.ElementAt(indexPath.Row).Name;

			return cell;
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var channel = this.ViewModel.Channels.ElementAt(indexPath.Row);
			this.NavigationService.Value.NavigateTo(nameof(MessagesViewModel), channel);
		}

		#endregion
	}
}