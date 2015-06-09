using System.Collections.Generic;
using System.Web.Http;
using ChatBackend.Helpers;
using ChatBackend.Models;

namespace ChatBackend.Controllers
{
	[RoutePrefix("api/messages")]
	public class MessagesController : ApiController
    {
		[Route("{team}/{channel}")]
		public void Post(string team, string channel, [FromBody] Message message)
		{
			var key = this.GetChannelKey(team, channel);
			RedisHelper.AddToCollection(key, message);
		}

		[Route("{team}/{channel}")]
		public IEnumerable<Message> Get(string team, string channel)
		{
			var key = this.GetChannelKey(team, channel);
			return RedisHelper.GetCollection<Message>(key);
		}

		private string GetChannelKey(string team, string channel)
		{
			return team + "." + channel;
		}
	}
}