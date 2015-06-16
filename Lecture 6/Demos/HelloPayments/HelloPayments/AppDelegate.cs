using Foundation;
using UIKit;
using LeanplumBindings;
using Xamarin;
using TaperecorderBinding;
using Facebook.CoreKit;

namespace HelloPayments
{
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			Leanplum.SetDevelopmentAppId("app_0rTalYoeicR4EfMQBDZAdBmpg1XeIwJzap7p2iBGiVI", "dev_IgFz41JDCoJq1KoDNeHm5ieBX5H3mSGOzcmq6OAhDW0");
			Leanplum.Start();

			Insights.Initialize("f9b1313070b01f3b9b850f653d30accc7c1a4139");

			TapeRecorder.StartRecording("d926a3d24630dccae275dbdd0688548f");

			Profile.EnableUpdatesOnAccessTokenChange (true);
			Settings.AppID = "1163660970326808";
			Settings.DisplayName = "Techent";
			return ApplicationDelegate.SharedInstance.FinishedLaunching (application, launchOptions);
		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			return ApplicationDelegate.SharedInstance.OpenUrl (application, url, sourceApplication, annotation);
		}
	}
}