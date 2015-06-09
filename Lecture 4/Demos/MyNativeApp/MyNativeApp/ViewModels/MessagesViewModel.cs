using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNativeApp
{
	[ExportToDI]
	public class MessagesViewModel : ViewModelBase
	{
		[Import]
		public Lazy<IUserService> UserService { get; set; }

		[Import]
		public Lazy<IRemoteClient> RemoteClient { get; set; }

		private IEnumerable<Message> messages = Enumerable.Empty<Message>();
		public IEnumerable<Message> Messages
		{ 
			get
			{
				return this.messages;
			}
			set
			{
				this.Set(value, ref this.messages);
			}
		}

		private string title;
		public override string Title
		{
			get
			{
				return this.title;
			}
		}

		private bool canReloadMessages;
		public bool CanReloadMessages
		{
			get
			{
				return this.canReloadMessages;
			}
			set
			{
				this.Set(value, ref this.canReloadMessages);
			}
		}

		public async Task ReloadMessages()
		{
			try
			{
				this.CanReloadMessages = false;
				this.Messages = this.RemoteClient.Value.GetMessages(this.Title);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				this.CanReloadMessages = true;
			}
		}

		private bool canSendMessage;
		public bool CanSendMessage
		{
			get
			{
				return this.canSendMessage;
			}
			set
			{
				this.Set(value, ref this.canSendMessage);
			}
		}

		private string message;
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.Set(value, ref this.message);
				this.CanSendMessage = !string.IsNullOrEmpty(message);
			}
		}

		public async Task SendMessage()
		{
			try
			{
				this.CanSendMessage = false;
				await this.RemoteClient.Value.SendMessage(this.Title, this.Message);
				await this.ReloadMessages();
				this.Message = string.Empty;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public override void NavigatedTo(params object[] parameters)
		{
			base.NavigatedTo(parameters);

			var channel = parameters.FirstOrDefault() as Channel;
			if (channel != null)
			{
				this.title = channel.Name;
			}
			else
			{
				throw new Exception("Channel name must be passed in the arguments!");
			}
		}
	}
}