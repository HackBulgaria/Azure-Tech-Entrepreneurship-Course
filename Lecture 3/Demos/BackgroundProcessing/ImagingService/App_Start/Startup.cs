using System.Collections.Generic;
using System.Web;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyWebApplication.Startup))]

namespace MyWebApplication
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			GlobalConfiguration.Configuration
				.UseSqlServerStorage("data source=NIKOLAIRINC9DE5;initial catalog=HangfireDb;persist security info=True;user id=niki;password=123;application name=EntityFramework;MultipleActiveResultSets=True");

			app.UseHangfireDashboard("/hangfire", new DashboardOptions
			{
				AuthorizationFilters = new[] { new CookieAuthFilter () }
			});
			app.UseHangfireServer();
		}

		/// <summary>
		/// This class was not in the demo, but was requested as an example how authentication works.
		/// It allows dashboard access to all localhost requests.
		/// If request is not localhost, it allows requests with a "MyHangfireAuthCookie" cookie that contains "MySuperSecretToken".
		/// </summary>
		private class CookieAuthFilter : IAuthorizationFilter
		{
			public bool Authorize(IDictionary<string, object> owinEnvironment)
			{
				object rawContext;
				if (owinEnvironment.TryGetValue("System.Web.HttpContextBase", out rawContext) &&
					rawContext is HttpContextWrapper)
				{
					var context = (HttpContextWrapper)rawContext;

					if (context.Request.Url.Host == "localhost")
					{
						// Authorize localhost requests
						return true;
					}

					// If not localhost - try see if auth cookie is present
					var cookie = context.Request.Cookies.Get("MyHangfireAuthCookie");
					if (cookie != null &&
						cookie.Value == "MySuperSecretToken")
					{
						return true;
					}
				}

				return false;
			}
		}
	}
}