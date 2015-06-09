using System;

namespace MyNativeApp
{
	public class Message
	{
		public string Text { get; set; }

		public string User { get; set; }

		public DateTimeOffset Date { get; set; }

		public bool IsCurrentUser { get; set; }
	}
}