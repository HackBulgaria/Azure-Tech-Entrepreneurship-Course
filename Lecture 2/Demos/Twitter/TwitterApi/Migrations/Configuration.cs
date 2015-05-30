namespace TwitterApi.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Common.Models;
	using TwitterApi.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<TwitterApi.Models.TwitterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(TwitterContext context)
		{
			context.Tweets.RemoveRange(context.Tweets);
			context.Friendships.RemoveRange(context.Friendships);
			context.Users.RemoveRange(context.Users);
			context.SaveChanges();

			var userNames = new[] { "Pesho", "Tosho", "Niki", "Valeri", "Vanya", "Maria", "Stan", "Jack", "Hristo" };
			var rand = new Random();
			foreach (var userName in userNames)
			{
				var user = new User
				{
					Birthday = new DateTimeOffset(rand.Next(1950, 2010), 1, 1, 0, 0, 0, TimeSpan.Zero),
					Email = userName.ToLower() + "@domain.com",
					FirstName = userName,
					LastName = userName + "v",
					Password = userName,
				};

				user.Tweets.Add(new Tweet
				{
					Date = new DateTimeOffset(2015, 5, rand.Next(1, 10), rand.Next(1, 20), 1, 1, TimeSpan.Zero),
					Text = "I am " + userName + " and this is my first Tweet!",
				});

				context.Users.Add(user);
			}

			context.SaveChanges();

			var tweet1 = context.Tweets.OrderBy(t => t.Date).First();
			var retweeters1 = context.Users.Where(u => u.Id != tweet1.UserId)
										   .OrderBy(u => u.FirstName)
										   .Take(3);

			foreach (var retweeter in retweeters1)
			{
				retweeter.Tweets.Add(new Tweet
				{
					Date = new DateTimeOffset(2015, 5, rand.Next(1, 10), rand.Next(1, 20), 1, 1, TimeSpan.Zero),
					Text = "Re: " + tweet1.Text,
					ParentId = tweet1.Id
				});
			}

			var tweet2 = context.Tweets.OrderBy(t => t.Date).Skip(1).First();
			var retweeters2 = context.Users.Where(u => u.Id != tweet2.UserId)
										   .OrderBy(u => u.Birthday)
										   .Take(5);

			foreach (var retweeter in retweeters2)
			{
				retweeter.Tweets.Add(new Tweet
				{
					Date = new DateTimeOffset(2015, 5, rand.Next(1, 10), rand.Next(1, 20), 1, 1, TimeSpan.Zero),
					Text = "Re: " + tweet2.Text,
					ParentId = tweet2.Id
				});
			}

			context.SaveChanges();

			var popular1 = context.Users.OrderBy(u => u.Birthday).First();
			var followers1 = context.Users.Where(u => u.Id != popular1.Id)
										 .OrderBy(u => u.Birthday)
										 .Take(3);

			foreach (var follower in followers1)
			{
				popular1.Followers.Add(new Friendship
				{
					Date = new DateTimeOffset(2015, 5, rand.Next(1, 10), rand.Next(1, 20), 1, 1, TimeSpan.Zero),
					FollowerId = follower.Id
				});
			}

			var popular2 = context.Users.OrderBy(u => u.Birthday).Skip(1).First();
			var followers2 = context.Users.Where(u => u.Id != popular2.Id)
										 .OrderBy(u => u.FirstName)
										 .Take(5);

			foreach (var follower in followers2)
			{
				popular2.Followers.Add(new Friendship
				{
					Date = new DateTimeOffset(2015, 5, rand.Next(1, 10), rand.Next(1, 20), 1, 1, TimeSpan.Zero),
					FollowerId = follower.Id
				});
			}

			context.SaveChanges();
		}
	}
}
