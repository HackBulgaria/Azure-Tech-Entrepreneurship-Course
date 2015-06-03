using System;
using Common;

namespace RedisProducer
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("I am producer");
			RedisHelper.Subscribe<string>("producer", message =>
			{
				Console.WriteLine(message);
			});

			while (true)
			{
				var message = Console.ReadLine();
				RedisHelper.Publish("worker", message);
				Console.WriteLine("Message sent!");
			}
		}
	}
}