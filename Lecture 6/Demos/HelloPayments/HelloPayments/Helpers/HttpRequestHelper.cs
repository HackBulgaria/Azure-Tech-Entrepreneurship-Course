using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelloPayments
{
	public static class HttpRequestHelper
	{
		public static Task<T> Get<T> (string endpoint, IDictionary<string, string> headers = null)
		{
			return Send<T> (HttpMethod.Get, endpoint, null, headers);
		}

		public static Task<HttpResponseMessage> Get (string endpoint, IDictionary<string, string> headers = null)
		{
			return Send (HttpMethod.Get, endpoint, null, headers);
		}

		public static Task<T> Post<T> (string endpoint, object payload = null, IDictionary<string, string> headers = null)
		{
			return Send<T> (HttpMethod.Post, endpoint, payload, headers);
		}

		public static Task<T> Post<T> (string endpoint, HttpContent payload, IDictionary<string, string> headers = null)
		{
			return Send<T> (HttpMethod.Post, endpoint, payload, headers);
		}

		public static Task<T> Patch<T> (string endpoint, IDictionary<string, object> payload = null, IDictionary<string, string> headers = null)
		{
			return Send<T> (new HttpMethod ("PATCH"), endpoint, payload, headers);
		}

		public static async Task Delete (string endpoint, IDictionary<string, string> headers = null)
		{
			await Send (HttpMethod.Delete, endpoint, null, headers);
		}

		public static Task<T> Send<T> (HttpMethod method, string endpoint, object payload, IDictionary<string, string> headers)
		{
			return Send<T> (method, endpoint, HttpContentHelper.GetJsonContent (payload), headers);
		}

		public static async Task<T> Send<T> (HttpMethod method, string endpoint, HttpContent payload, IDictionary<string, string> headers)
		{
			var response = await Send (method, endpoint, payload, headers);
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

		private static async Task<HttpResponseMessage> Send (HttpMethod method, string endpoint, HttpContent content, IDictionary<string, string> headers)
		{
			using (var client = new HttpClient ())
			{
				var request = new HttpRequestMessage (method, endpoint);
				if (content != null && method != HttpMethod.Get)
				{
					request.Content = content;
				}

				if (headers != null)
				{
					foreach (var header in headers)
					{
						request.Headers.TryAddWithoutValidation (header.Key, header.Value);
					}
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