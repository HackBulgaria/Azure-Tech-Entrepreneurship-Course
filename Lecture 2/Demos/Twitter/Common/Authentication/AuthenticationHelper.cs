using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authentication
{
	public static class AuthenticationHelper
	{
		private const string Salt = "123";

		public static string Encrypt(string unencrypted)
		{
			return unencrypted + Salt;
		}

		public static string Decrypt(string encrypted)
		{
			return encrypted.Substring(0, encrypted.Length - Salt.Length);
		}
	}
}
