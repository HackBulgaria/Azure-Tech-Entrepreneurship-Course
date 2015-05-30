using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class User
	{
		[Key]
		public long Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTimeOffset Birthday { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public virtual ICollection<Friendship> Followers { get; set; }

		public virtual ICollection<Friendship> Following { get; set; }

		public virtual ICollection<Tweet> Tweets { get; set; }

		public User()
		{
			this.Followers = new HashSet<Friendship>();
			this.Following = new HashSet<Friendship>();
			this.Tweets = new HashSet<Tweet>();
		}
	}
}
