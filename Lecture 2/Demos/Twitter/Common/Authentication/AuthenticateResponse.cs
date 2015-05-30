using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authentication
{
	public class AuthenticateResponse
	{
		public long Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Token { get; set; }
	}
}
