using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;

namespace HelloJenkins.Controllers.Tests
{
	[TestClass()]
	public class HelloControllerTests
	{
		private HelloController controller;

		[TestInitialize]
		public void Prepare()
		{
			this.controller = new HelloController();
			this.controller.ControllerContext.Configuration = new System.Web.Http.HttpConfiguration();
			this.controller.Request = new System.Net.Http.HttpRequestMessage();
		}

		[TestMethod()]
		public async Task PostTest()
		{
			var result = await this.controller.Post("Niki").ExecuteAsync(CancellationToken.None);
			var payload = await result.Content.ReadAsStringAsync();
			var deserialized = JsonConvert.DeserializeAnonymousType(payload, new { Message = string.Empty });
			Assert.AreEqual(deserialized.Message, "Hello, Niki, you are awesome!");
		}

		[TestCleanup]
		public void Cleanup()
		{
			this.controller = null;
		}
	}
}