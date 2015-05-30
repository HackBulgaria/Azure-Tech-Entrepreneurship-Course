using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using Common.Models;

namespace TwitterApi.Controllers
{
	[Export]
	public class TweetsController : ODataControllerBase<Tweet>
	{
		[EnableQuery]
		public SingleResult<Tweet> GetTweet(long key)
		{
			return this.Get(key);
		}

		protected override Expression<Func<Tweet, bool>> GetByIdExpression(long key)
		{
			return t => t.Id == key;
		}
	}
}