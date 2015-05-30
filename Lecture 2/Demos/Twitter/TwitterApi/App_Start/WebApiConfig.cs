using System.Collections.Generic;
using System.Composition;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Common.Authentication;
using Common.Models;
using Microsoft.OData.Edm;
using TwitterApi.Controllers;

namespace TwitterApi
{
	[Export, Shared]
	public class WebApiConfig
	{
		[ImportMany]
		public IEnumerable<IFilter> Filters { get; set; }

		public void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			config.Filters.AddRange(this.Filters);
			// Web API routes
			config.MapHttpAttributeRoutes();

			config.MapODataServiceRoute("odata", "api", GetModel());

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}

		private static IEdmModel GetModel()
		{
			const string UsersSet = "Users";
			const string FriendshipsSet = "Friendships";
			const string TweetsSet = "Tweets";

			var builder = new ODataConventionModelBuilder();
			var users = builder.EntitySet<User>(UsersSet);
			builder.EntitySet<Friendship>(FriendshipsSet);
			builder.EntitySet<Tweet>(TweetsSet);

			users.EntityType.Ignore(u => u.Password);
			users.EntityType.Ignore(u => u.Email);

			// nameof(...) == "Login"
			builder.Namespace = "TwitterApi";
			var login = users.EntityType.Collection.Action(nameof(UsersController.Login));
			login.Parameter<string>(nameof(User.Password));
			login.Parameter<string>(nameof(User.Email));
			login.Returns<AuthenticateResponse>();

			var me = users.EntityType.Collection.Function(nameof(UsersController.Me));
			me.ReturnsFromEntitySet<User>(UsersSet);

			return builder.GetEdmModel();
		}
	}
}