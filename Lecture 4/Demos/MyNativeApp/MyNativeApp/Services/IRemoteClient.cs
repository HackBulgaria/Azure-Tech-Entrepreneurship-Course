using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNativeApp
{
	public interface IRemoteClient
	{
		Task CreateChannel(string name);

		Task<IEnumerable<Channel>> GetChannels();

		Task SendMessage(string channel, string message);

		Task<IEnumerable<Message>> GetMessages(string channel);
	}
}