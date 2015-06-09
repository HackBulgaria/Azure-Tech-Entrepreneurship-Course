// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MyNativeApp.iOS
{
	[Register ("ChannelViewController")]
	partial class ChannelViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem AddChannelButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem ReloadChannelsButton { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}

			if (AddChannelButton != null) {
				AddChannelButton.Dispose ();
				AddChannelButton = null;
			}

			if (ReloadChannelsButton != null) {
				ReloadChannelsButton.Dispose ();
				ReloadChannelsButton = null;
			}
		}
	}
}
