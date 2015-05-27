using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechEntWebApp.Models
{
	public class User
	{
		[Key]
		public long Id { get; set; }

		public string Name { get; set; }
	}
}