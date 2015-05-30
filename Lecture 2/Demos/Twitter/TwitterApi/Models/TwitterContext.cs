using System.Composition;
using System.Data.Entity;
using Common.Models;

namespace TwitterApi.Models
{
	[Export]
	public class TwitterContext : DbContext
	{
		public virtual DbSet<User> Users { get; set; }

		public virtual DbSet<Tweet> Tweets { get; set; }

		public virtual DbSet<Friendship> Friendships { get; set; }

		public TwitterContext() : base("name=TwitterDb")
		{

		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
						.HasMany(u => u.Followers)
						.WithRequired(u => u.Followed)
						.HasForeignKey(u => u.FollowedId)
						.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
						.HasMany(u => u.Following)
						.WithRequired(u => u.Follower)
						.HasForeignKey(u => u.FollowerId)
						.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
						.HasMany(u => u.Tweets)
						.WithRequired(u => u.User)
						.HasForeignKey(u => u.UserId)
						.WillCascadeOnDelete(false);

			modelBuilder.Entity<Tweet>()
						.HasMany(u => u.Retweets)
						.WithOptional(u => u.Parent)
						.HasForeignKey(u => u.ParentId)
						.WillCascadeOnDelete(false);
		}
	}
}