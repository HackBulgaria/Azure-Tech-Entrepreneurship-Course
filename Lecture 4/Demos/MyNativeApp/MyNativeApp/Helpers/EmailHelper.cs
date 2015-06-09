using System.Net.Mail;

namespace MyNativeApp
{
	[ExportToDI, Shared]
	public class EmailHelper
	{
		public bool IsEmailValid(string email)
		{
			try 
			{
				return new MailAddress(email).Address == email;
			}
			catch 
			{
				return false;
			}
		}

		public string GetHost(string email)
		{
			return new MailAddress(email).Host;
		}

		public string GetUser(string email)
		{
			return new MailAddress(email).User;
		}
	}
}