using System.Collections.Generic;
using System.Web.Http;
using ChatBackend.Helpers;
using ChatBackend.Models;

namespace ChatBackend.Controllers
{
	[RoutePrefix("api/channels")]
    public class ChannelsController : ApiController
    {
		[Route("{team}")]
		public void Post(string team, [FromBody] Channel channel)
		{
			RedisHelper.AddToCollection(team, channel);
		}

		[Route("{team}")]
		public IEnumerable<Channel> Get(string team)
		{
			return RedisHelper.GetCollection<Channel>(team);
		}

		public void Delete()
		{
			RedisHelper.CleanUp();
		}
    }
}