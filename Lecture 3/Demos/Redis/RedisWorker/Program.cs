using System;
using Common;

namespace RedisWorker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("I am worker");
			RedisHelper.Subscribe<string>("worker", message =>
			{
				Console.WriteLine(message);
			});

			while (true)
			{
				var message = Console.ReadLine();
				RedisHelper.Publish("producer", message);
				Console.WriteLine("Message sent!");
			}
		}
	}
}