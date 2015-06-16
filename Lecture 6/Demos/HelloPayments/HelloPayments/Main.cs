using UIKit;

namespace HelloPayments
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");
		}
	}

	public static class Constants
	{
		public const string ServerApiAddress = "http://192.168.0.162/hellopayments/api";
	}
}