using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MyNativeApp
{
	[ExportToDI(typeof(IRemoteClient))]
	public class RemoteClient : IoCAwareBase, IRemoteClient
	{
		[Import]
		public Lazy<IUserService> UserService { get; set; }

		[Import]
		public Lazy<HttpHelper> HttpHelper { get; set; }

		private const string ServerAddress = "http://chatbackend.azurewebsites.net/api";

		public Task CreateChannel(string name)
		{
			var endpoint = string.Format("{0}/channels/{1}", ServerAddress, this.UserService.Value.TeamId);
			return this.HttpHelper.Value.Post<object>(endpoint, new Channel { Name = name });

		}
	
		public async Task<IEnumerable<Channel>> GetChannels()
		{
			var endpoint = string.Format("{0}/channels/{1}", ServerAddress, this.UserService.Value.TeamId);
			var channels = await this.HttpHelper.Value.Get<IEnumerable<Channel>>(endpoint);
			return channels.OrderBy(c => c.Name).ToArray();
		}

		public Task SendMessage(string channel, string message)
		{
			var endpoint = string.Format("{0}/messages/{1}/{2}", ServerAddress, this.UserService.Value.TeamId, channel);
			var payload = new Message
				{
					Date = DateTimeOffset.UtcNow,
					Text = message,
					User = this.UserService.Value.UserId
				};
			return this.HttpHelper.Value.Post<object>(endpoint, payload);
		}

		public async Task<IEnumerable<Message>> GetMessages(string channel)
		{
			var endpoint = string.Format("{0}/messages/{1}/{2}", ServerAddress, this.UserService.Value.TeamId, channel);
			var messages = await this.HttpHelper.Value.Get<IEnumerable<Message>>(endpoint);
			foreach (var message in messages)
			{
				message.IsCurrentUser = this.UserService.Value.UserId == message.User;
			}
			return messages.OrderByDescending(c => c.Date).ToArray();
		}
	}
}