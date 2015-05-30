using System;
using System.Collections.Generic;
using System.Composition;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using Common.Models;
using TwitterApi.Authentication;
using TwitterApi.Models;

namespace TwitterApi.Controllers
{
	public abstract class ODataControllerBase<TEntity> : ODataController where TEntity : class
	{
		public DbSet<TEntity> Entities
		{
			get
			{
				return this.Context.Set<TEntity>();
            }
		}

		[Import]
		public TwitterContext Context { get; set; }

		[EnableQuery]
		public IQueryable<TEntity> Get()
		{
			return this.Entities;
		}

		public IHttpActionResult Post(TEntity entity)
		{
			this.Entities.Add(entity);
			this.Context.SaveChanges();

			return this.Created(entity);
		}

		public IHttpActionResult Patch(long key, Delta<TEntity> patch)
		{
			var entity = this.GetEntitiesById(key).FirstOrDefault();
			if (entity == null)
			{
				return this.NotFound();
			}

			patch.Patch(entity);
			this.Context.SaveChanges();
			return this.Updated(entity);
		}

		public IHttpActionResult Delete([FromODataUri] long key)
		{
			var entity = this.GetEntitiesById(key).FirstOrDefault();
			if (entity == null)
			{
				return this.NotFound();
			}

			this.Entities.Remove(entity);
			this.Context.SaveChanges();

			return this.StatusCode(HttpStatusCode.NoContent);
		}

		protected SingleResult<TEntity> Get(long key)
		{
			return SingleResult.Create(this.GetEntitiesById(key));
		}

		protected abstract Expression<Func<TEntity, bool>> GetByIdExpression(long key);

		protected IQueryable<TEntity> GetEntitiesById(long key)
		{
			return this.Entities.Where(this.GetByIdExpression(key));
        }

		protected User GetCurrentUser()
		{
			var id = this.User?.Identity as GenericIdentity;
			if (id != null)
			{
				return this.Context.Users.FirstOrDefault(u => u.Id == id.UserId);
			}

			return null;
		}
	}
}