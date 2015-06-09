using Foundation;
using UIKit;
using System;

namespace MyNativeApp.iOS
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		[Import]
		public Lazy<INavigationService> NavigationService { get; set; }

		[Import]
		public Lazy<IDialogService> DialogService { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			var navigationController = this.Window.RootViewController as UINavigationController;
			if (navigationController != null)
			{
				this.ResolveDependencies();
				this.NavigationService.Value.Initialize(navigationController);
				this.DialogService.Value.Initialize(navigationController);
			}
			else
			{
				throw new Exception("NavigationService not initialized!");
			}

			return true;
		}
	}
}