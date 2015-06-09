namespace MyNativeApp
{
	public partial interface INavigationService
	{
		void NavigateTo(string key, params object[] parameters);
	}
}