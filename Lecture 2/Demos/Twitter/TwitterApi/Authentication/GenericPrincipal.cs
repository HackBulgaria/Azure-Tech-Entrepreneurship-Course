using System.Security.Principal;

namespace TwitterApi.Authentication
{
	public class GenericPrincipal : IPrincipal
	{
		public IIdentity Identity { get; private set; }

		public GenericPrincipal(long id)
		{
			this.Identity = new GenericIdentity(id);
		}

		public bool IsInRole(string role)
		{
			return true;
		}
	}

	public class GenericIdentity : IIdentity
	{
		public string AuthenticationType
		{
			get
			{
				return "Token";
			}
		}

		public bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		public string Name
		{
			get
			{
				return string.Empty;
			}
		}

		public long UserId { get; set; }

		public GenericIdentity(long id)
		{
			this.UserId = id;
		}
	}
}