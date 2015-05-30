using System;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Authentication;
using TwitterApi.Authentication;
using TwitterApi.Models;

namespace TwitterApi
{
	[Export(typeof(IFilter)), Shared]
	public class TwitterAuthorizationFilter : AuthorizationFilterAttribute
	{
		[Import]
		public ILogger Logger { get; set; }

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			var header = actionContext.Request.Headers.Authorization;
			if (header != null && header.Scheme == "Token")
			{
				var id = AuthenticationHelper.Decrypt(header.Parameter);
				this.Logger.Log(id);
				Thread.CurrentPrincipal =
				HttpContext.Current.User = new GenericPrincipal(long.Parse(id));
			}
		}
	}

	public interface ILogger
	{
		void Log(string text);
	}

	[Export(typeof(ILogger)), Shared]
	public class Logger : ILogger
	{
		public void Log(string text)
		{
			Debug.WriteLine(text);
		}
	}
}