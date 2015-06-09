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
	[Register ("MessagesController")]
	partial class MessagesController
	{
		[Outlet]
		UIKit.UITextField NewMessageTextField { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem RefreshButton { get; set; }

		[Outlet]
		UIKit.UIButton SendButton { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NewMessageTextField != null) {
				NewMessageTextField.Dispose ();
				NewMessageTextField = null;
			}

			if (SendButton != null) {
				SendButton.Dispose ();
				SendButton = null;
			}

			if (RefreshButton != null) {
				RefreshButton.Dispose ();
				RefreshButton = null;
			}

			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}
		}
	}
}
