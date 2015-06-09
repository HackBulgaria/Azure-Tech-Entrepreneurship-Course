using System.Threading.Tasks;
using UIKit;

namespace MyNativeApp
{
	public partial interface IDialogService
	{
		void Initialize(UINavigationController navigationController);
	}
}

namespace MyNativeApp.iOS
{
	[ExportToDI(typeof(IDialogService)), Shared]
	public class DialogService : IoCAwareBase, IDialogService
	{
		private UINavigationController navigationController;

		public void Initialize(UINavigationController navigationController)
		{
			this.navigationController = navigationController;
		}

		public Task<string> PromptForInput(string title, string message)
		{
			var tcs = new TaskCompletionSource<string>();
			var alertVC = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alertVC.AddTextField(tf =>
				{
					tf.Placeholder = "Channel name";
				});
			alertVC.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) =>
				{
					var input = alertVC.TextFields[0].Text;
					tcs.TrySetResult(input);
				}));
			alertVC.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (action) =>
				{
					tcs.TrySetCanceled();
				}));

			this.navigationController.PresentViewController(alertVC, true, null);
			return tcs.Task;
		}
	}
}