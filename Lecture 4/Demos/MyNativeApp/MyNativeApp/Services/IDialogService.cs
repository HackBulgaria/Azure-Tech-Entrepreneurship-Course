using System.Threading.Tasks;

namespace MyNativeApp
{
	public partial interface IDialogService
	{
		Task<string> PromptForInput(string title, string message);
	}
}