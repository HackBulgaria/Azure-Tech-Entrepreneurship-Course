using System;
using System.Threading.Tasks;
using Common.Models;
using Simple.OData.Client;

namespace TwitterClient
{
	class Program
	{
		static void Main(string[] args)
		{
			GetTweets().Wait();
			Console.Read();
		}

		private static async Task GetTweets()
		{
			var client = new ODataClient("http://localhost/TwitterApi/api");
			var result = await client.For<Tweet>()
									 .Filter(f => f.Date > new System.DateTimeOffset(2015, 5, 2, 0, 0, 0, TimeSpan.Zero))
									 .OrderBy(f => f.User.FirstName)
									 .Expand(f => f.User)
									 .FindEntriesAsync();

			foreach (var tweet in result)
			{
				Console.WriteLine("{0}: {1}", tweet.User.FirstName, tweet.Text);
			}
		}
	}
}