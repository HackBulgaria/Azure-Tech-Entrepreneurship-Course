using System;

namespace MyNativeApp
{
	[ExportToDIAttribute]
	public class MainViewModel : ViewModelBase
	{
		[Import]
		public Lazy<INavigationService> NavigationService { get; set; }

		[Import]
		public Lazy<EmailHelper> EmailHelper { get; set; }

		[Import]
		public Lazy<IUserService> UserService { get; set; }

		public override string Title
		{
			get
			{
				return "Login";
			}
		}

		private string userEmail;
		public string UserEmail
		{
			get
			{
				return this.userEmail;
			}
			set
			{
				this.Set(value, ref this.userEmail);
				this.CanLogin = this.EmailHelper.Value.IsEmailValid(this.UserEmail);
			}
		}

		private bool canLogin;
		public bool CanLogin
		{
			get
			{
				return this.canLogin;
			}
			set
			{
				this.Set(value, ref this.canLogin);
			}
		}

		public void Login()
		{
			this.UserService.Value.UserEmail = this.UserEmail;
			this.NavigationService.Value.NavigateTo(nameof(ChannelViewModel));
		}
	}
}