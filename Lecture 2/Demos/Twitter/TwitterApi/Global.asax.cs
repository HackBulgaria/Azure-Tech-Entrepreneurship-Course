using System.Composition.Hosting;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Common.Authentication;
using Common.Models;
using TwitterApi.Composition;

namespace TwitterApi
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start()
		{
			SetupAutoMapper();
			var container = new ContainerConfiguration()
									.WithAssembly(typeof(WebApiApplication).Assembly)
									.CreateContainer();

			var resolver = new StandaloneDependencyResolver(container);
			GlobalConfiguration.Configuration.DependencyResolver = resolver;
			GlobalConfiguration.Configure(container.GetExport<WebApiConfig>().Register);
		}

		private static void SetupAutoMapper()
		{
			Mapper.CreateMap<User, AuthenticateResponse>()
				.ForMember(a => a.Token, opt => opt.MapFrom(u => AuthenticationHelper.Encrypt(u.Id.ToString())));
		}
	}
}