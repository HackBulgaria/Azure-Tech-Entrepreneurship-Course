using System.Web.Http;

namespace TechEntWebApp.Controllers
{
	[RoutePrefix("api/shorten")]
    public class ShortenController : ApiController
    {
		[Route("{key}")]
		[HttpPost]
		public void Shorten(string key, [FromBody] string url)
		{
			RedisHelper.Set(key, url);
        }
    }
}