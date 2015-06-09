using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MyNativeApp
{
	[ExportToDI, Shared]
	public class HttpHelper
	{
		public Task<T> Get<T> (string endpoint)
		{
			return Send<T>(HttpMethod.Get, endpoint, null);
		}

		public Task<T> Post<T> (string endpoint, object payload)
		{
			HttpContent content = null;
			if (payload != null)
			{
				content = new StringContent(JsonConvert.SerializeObject(payload));
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			}

			return Send<T>(HttpMethod.Post, endpoint, content);
		}

		public async Task<T> Send<T> (HttpMethod method, string endpoint, HttpContent payload)
		{
			var response = await Send (method, endpoint, payload);
			var content = await response.Content.ReadAsStringAsync ();
			try
			{
				return JsonConvert.DeserializeObject<T> (content);
			}
			catch
			{
				return default(T);
			}
		}

		private async Task<HttpResponseMessage> Send (HttpMethod method, string endpoint, HttpContent content)
		{
			using (var client = new HttpClient ())
			{
				var request = new HttpRequestMessage (method, endpoint);
				if (content != null && method != HttpMethod.Get)
				{
					request.Content = content;
				}

				var response = await client.SendAsync (request);
				await CheckIsSuccess (response, string.Format ("Unable to {0} to {1}", method.Method, endpoint));
				return response;
			}
		}

		private static async Task CheckIsSuccess (HttpResponseMessage response, string message)
		{
			if (!response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync ();

				throw new Exception (string.Format ("{0}\nStatus code: {1}\nContent: {2}", message, response.StatusCode, content));
			}
		}
	}
}