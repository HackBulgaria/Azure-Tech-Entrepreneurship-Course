using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TechEntWebApp.Models
{
	public class MyDbContext : DbContext
	{
		public virtual DbSet<User> Users { get; set; }

		public MyDbContext() : base("name=MyDbContext")
		{
		}
	}
}