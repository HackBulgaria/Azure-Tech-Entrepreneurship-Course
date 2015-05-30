using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class Tweet
	{
		[Key]
		public long Id { get; set; }

		public string Text { get; set; }

		public DateTimeOffset Date { get; set; }

		public long UserId { get; set; }

		public long? ParentId { get; set; }

		public virtual User User { get; set; }

		public virtual Tweet Parent { get; set; }

		public virtual ICollection<Tweet> Retweets { get; set; }

		public Tweet()
		{
			this.Retweets = new HashSet<Tweet>();
		}
	}
}
