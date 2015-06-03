using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageUploader
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();
			foreach (var file in Directory.EnumerateFiles("ImagesToUpload"))
			{
				Upload(file).Wait();
			}
			sw.Stop();
			Console.WriteLine("Elapsed: {0} s", sw.Elapsed.TotalSeconds);
			Console.ReadLine();
		}

		private static async Task Upload(string file)
		{
			using (var client = new HttpClient())
			{
				var content = new MultipartFormDataContent();
				content.Add(new StreamContent(File.Open(file, FileMode.Open)), "image", Path.GetFileNameWithoutExtension(file));
                var response = await client.PostAsync("http://localhost/ImagingService/api/images/", content);
			}
		}
	}
}