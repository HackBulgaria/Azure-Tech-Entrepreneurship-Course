using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Hangfire;
using ImagingService.Helpers;

namespace ImagingService.Controllers
{
	public class ImagesController : ApiController
    {
		// Since we have no API to define custom Hangfire job id's and the client should not be aware of 
		// the generated ids, we store a mapping from imageName -> jobId. Obviously this is a very naive
		// approach since it won't survive server restarts. It would be better to store it in Redis or
		// another persistent store where it is accessible from other server instances as well.
		private static ConcurrentDictionary<string, string> nameToJobIdMapping = new ConcurrentDictionary<string, string>();

		[HttpPost]
		[Route("api/images")]
		public async Task<IHttpActionResult> Post()
		{
			if (!this.Request.Content.IsMimeMultipartContent())
			{
				throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
			}

			var provider = new MultipartMemoryStreamProvider();
			await this.Request.Content.ReadAsMultipartAsync(provider);
			foreach (var file in provider.Contents)
			{
				var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
				var buffer = await file.ReadAsByteArrayAsync();

				ImageHelper.SaveOriginal(filename, buffer);
				var id = BackgroundJob.Enqueue(() => ImageHelper.StoreAndResize(filename));
				BackgroundJob.ContinueWith(id, () => Debug.WriteLine("Resize complete", filename));
				
				// New code to enable getting current status
				nameToJobIdMapping.AddOrUpdate(filename, id, (oldKey, oldValue) => id);
			}

			return this.Ok();
		}

		/// <summary>
		/// This was not on the demo, but someone requested to see how they can use Hangfire's monitoring API to check status of a background job.
		/// To query for the current job status, simple execute GET http://localhost/imagingservice/api/images/earth/status
		/// Keep in mind that building/running will restart the application thus any previous info stored in nameToJobIdMapping will be lost.
		/// </summary>
		[HttpGet]
		[Route("api/images/{name}/status")]
		public IHttpActionResult GetStatus(string name)
		{
			string jobId;
			if (nameToJobIdMapping.TryGetValue(name, out jobId))
			{
				var details = JobStorage.Current.GetMonitoringApi().JobDetails(jobId);
				var currentState = details?.History.OrderByDescending(h => h.CreatedAt).FirstOrDefault();
				// currentState is the last state of the job - you can use it to check if it's completed/currently processing, etc.
				if (currentState != null)
				{
					return this.Ok(currentState);
				}
			}

			return this.NotFound();
		}


		[HttpGet]
		[Route("api/images/{name}")]
		public HttpResponseMessage Get(string name, int size = 1024)
		{
			var file = ImageHelper.Get(name, size);
			var content = new StreamContent(file);
			content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

			return new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = content,
			};
		}
    }
}