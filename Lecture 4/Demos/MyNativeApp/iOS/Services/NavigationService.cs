using UIKit;

namespace MyNativeApp
{
	public partial interface INavigationService
	{
		void Initialize(UINavigationController navigationController);
	}
}

namespace MyNativeApp.iOS
{
	[ExportToDI(typeof(INavigationService)), Shared]
	public class NavigationService : IoCAwareBase, INavigationService
	{
		private UINavigationController navigationController;

		public void Initialize(UINavigationController navigationController)
		{
			this.navigationController = navigationController;
		}

		public void NavigateTo(string key, params object[] parameters)
		{
			var vcName = key.Replace("Model", "Controller");

			var targetVC = this.navigationController.Storyboard.InstantiateViewController(vcName);
			this.navigationController.PushViewController(targetVC, true);


			if(parameters != null)
			{
				var iocAwareVC = targetVC as IModeledController;
				if (iocAwareVC != null)
				{
					iocAwareVC.Model.NavigatedTo(parameters);
				}
			}
		}
	}
}