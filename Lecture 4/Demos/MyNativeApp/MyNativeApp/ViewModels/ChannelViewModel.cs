using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNativeApp
{
	[ExportToDIAttribute]
	public class ChannelViewModel : ViewModelBase
	{
		[Import]
		public Lazy<IUserService> UserService { get; set; }

		[Import]
		public Lazy<IRemoteClient> RemoteClient { get; set; }

		[Import]
		public Lazy<IDialogService> DialogService { get; set; }

		private IEnumerable<Channel> channels = Enumerable.Empty<Channel>();
		public IEnumerable<Channel> Channels
		{ 
			get
			{
				return this.channels;
			}
			set
			{
				this.Set(value, ref this.channels);
			}
		}

		public override string Title
		{
			get
			{
				return "Channels";
			}
		}

		private bool canReloadChannels;
		public bool CanReloadChannels
		{
			get
			{
				return this.canReloadChannels;
			}
			set
			{
				this.Set(value, ref this.canReloadChannels);
			}
		}

		private bool canCreateChannel = true;
		public bool CanCreateChannel
		{
			get
			{
				return this.canCreateChannel;
			}
			set
			{
				this.Set(value, ref this.canCreateChannel);
			}
		}

		public async Task ReloadChannels()
		{
			try
			{
				this.CanReloadChannels = false;
				this.Channels = await this.RemoteClient.Value.GetChannels();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				this.CanReloadChannels = true;
			}
		}

		public async Task CreateChannel()
		{
			try
			{
				this.CanCreateChannel = false;
				var channelName = await this.DialogService.Value.PromptForInput("Channel name", "Specify the new channel name");
				await this.RemoteClient.Value.CreateChannel(channelName);
				await this.ReloadChannels();
			}
			catch (TaskCanceledException)
			{
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				this.CanCreateChannel = true;
			}
		}
	}
}