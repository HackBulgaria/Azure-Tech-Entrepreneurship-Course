using System.Web.Mvc;

namespace TechEntWebApp.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(string id)
		{
			string url;
			if (RedisHelper.TryGet(id, out url))
			{
				return this.Redirect(url);
			}

			this.ViewBag.Title = "Home Page";

			return this.View();
		}
	}
}
