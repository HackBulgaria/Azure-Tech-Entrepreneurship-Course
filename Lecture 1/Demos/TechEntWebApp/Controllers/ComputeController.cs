using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Web.Hosting;
using System.Web.Http;

namespace TechEntWebApp.Controllers
{
	public class ComputeController : ApiController
    {
		private static readonly Guid serverId = Guid.NewGuid();

		public ZipResponse Zip()
		{
			var sw = new Stopwatch();
			sw.Start();
			var folder = HostingEnvironment.MapPath("~/Resources");
			ZipFile.CreateFromDirectory(Path.Combine(folder, "Files"), Path.Combine(folder, Guid.NewGuid() + ".zip"), CompressionLevel.Optimal, true);
			sw.Stop();
			return new ZipResponse
			{
				Elapsed = sw.ElapsedMilliseconds,
				ServerId = serverId
			};
		}
    }

	public class ZipResponse
	{
		public Guid ServerId { get; set; }

		public long Elapsed { get; set; }
	}
}
