namespace MyNativeApp
{
	public interface INavigationAware
	{
		void NavigatedTo(params object[] parameters);
	}
}