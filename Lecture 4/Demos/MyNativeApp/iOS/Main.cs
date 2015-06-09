using UIKit;

namespace MyNativeApp.iOS
{
	public class Application
	{
		static void Main (string[] args)
		{
			typeof(Application).Assembly.RegisterExports();
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}