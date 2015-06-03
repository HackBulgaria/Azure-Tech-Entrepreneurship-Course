using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using ImagingService.Helpers;

namespace ImagingService.Controllers
{
	public class ImagesController : ApiController
    {
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
				ImageHelper.StoreAndResize(filename, buffer);
			}

			return this.Ok();
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