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
									 .OrderBy(f => f.User.FirstName) // See bottom comment why this doesn't work. 
									 .Expand(f => f.User)
									 .FindEntriesAsync();

			foreach (var tweet in result)
			{
				Console.WriteLine("{0}: {1}", tweet.User.FirstName, tweet.Text);
			}
		}
	}
}

/*
	It seems there'a s bug with the way Simple OData Client generates the url for the orderby clause
	The proper url should look like: http://localhost/TwitterApi/api/Tweets?$expand=User&$orderby=User/FirstName
	Whereas the generated url is: http://localhost/TwitterApi/api/Tweets?$expand=User($orderby=FirstName)

	Apparently, it infers that the User navigation property is a collection and that should be ordered instead of the original collection.
*/