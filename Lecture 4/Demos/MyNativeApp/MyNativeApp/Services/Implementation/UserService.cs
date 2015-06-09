using System;
using System.Globalization;

namespace MyNativeApp
{
	[ExportToDI(typeof(IUserService)), Shared]
	public class UserService : IoCAwareBase, IUserService
	{
		[Import]
		public Lazy<EmailHelper> EmailHelper { get; set; }

		public string UserEmail { get; set; }

		public string TeamId
		{
			get
			{
				return this.EmailHelper.Value.GetHost(this.UserEmail).Replace(".", "_");
			}
		}
		public string UserId
		{
			get
			{
				return this.EmailHelper.Value.GetUser(this.UserEmail);
			}
		}
	}
}