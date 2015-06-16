// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HelloPayments
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton BuyButton { get; set; }

		[Outlet]
		UIKit.UITextField EmailInputField { get; set; }

		[Action ("ShowFeedback:")]
		partial void ShowFeedback (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BuyButton != null) {
				BuyButton.Dispose ();
				BuyButton = null;
			}

			if (EmailInputField != null) {
				EmailInputField.Dispose ();
				EmailInputField = null;
			}
		}
	}
}
