using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyLoadTester
{
	class Program
	{
		static void Main(string[] args)
		{
			while(true)
			{
				Task.WaitAll(
					Test(),
					Test(),
					Test(),
					Test(),
					Test(),
					Test());
			}
		}

		private static async Task Test()
		{
			using (var client = new HttpClient())
			{
				var response = await client.PostAsync("http://localhost/techentwebapp/api/compute/zip", new StringContent(string.Empty));
				Console.WriteLine(await response.Content.ReadAsStringAsync());
			}
		}
	}
}