using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
	public class Friendship
	{
		[Key]
		[Column(Order = 0)]
		public long FollowerId { get; set; }

		[Key]
		[Column(Order = 1)]
		public long FollowedId { get; set; }

		public DateTimeOffset Date { get; set; }

		public virtual User Follower { get; set; }

		public virtual User Followed { get; set; }
	}
}
