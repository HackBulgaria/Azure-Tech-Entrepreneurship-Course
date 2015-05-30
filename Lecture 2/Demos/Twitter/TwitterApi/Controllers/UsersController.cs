using System;
using System.Composition;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using AutoMapper;
using Common.Authentication;
using Common.Models;
using TwitterApi.Models;

namespace TwitterApi.Controllers
{
	[Export]
	public class UsersController : ODataControllerBase<User>
    {
        [EnableQuery]
        public SingleResult<User> GetUser(long key)
        {
			return this.Get(key);
        }

        [EnableQuery]
        public IQueryable<User> GetFollowers(long key)
        {
            return this.GetEntitiesById(key).SelectMany(m => m.Followers.Select(f => f.Follower));
        }

        [EnableQuery]
        public IQueryable<User> GetFollowing(long key)
        {
			return this.GetEntitiesById(key).SelectMany(m => m.Followers.Select(f => f.Followed));
		}

		[EnableQuery]
        public IQueryable<Tweet> GetTweets(long key)
        {
            return this.GetEntitiesById(key).SelectMany(m => m.Tweets);
        }

		[HttpPost]
		public IHttpActionResult Login(ODataActionParameters parameters)
		{
			var email = parameters.GetValue<string>(nameof(Common.Models.User.Email));
			var password = parameters.GetValue<string>(nameof(Common.Models.User.Password));

			var user = this.Entities.FirstOrDefault(u => u.Email == email && u.Password == password);
			if (user == null)
			{
				return this.Unauthorized();
			}

			return this.Ok(Mapper.Map<AuthenticateResponse>(user));
		}

		[HttpGet]
		[EnableQuery]
		public IHttpActionResult Me()
		{
			var user = this.GetCurrentUser();
			if (user != null)
			{
				return this.Ok(user);
			}

			return this.NotFound();
		}

		protected override Expression<Func<User, bool>> GetByIdExpression(long key)
		{
			return u => u.Id == key;
		}
    }
}