using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqProducer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("I am producer");
			var factory = new ConnectionFactory
			{
				HostName = "localhost",
				UserName = "guest",
				Password = "123"
			};
			var model = factory.CreateConnection().CreateModel();

			DemoQueue(model);
		}

		// Broadcasts messages to anyone who would listen (Radio/TV)
		static void DemoExchange(IModel model)
		{
			model.ExchangeDeclare("myexchange", "fanout");

			while (true)
			{
				var message = Console.ReadLine();
				var body = Encoding.UTF8.GetBytes(message);
				model.BasicPublish("myexchange", "", null, body);
			}
		}

		// Sends a message to a queue to be handled by available worker
		static void DemoQueue(IModel model)
		{
			model.QueueDeclare("myqueue", true, false, false, null);

			var properties = model.CreateBasicProperties();
			properties.SetPersistent(true);

			while (true)
			{
				var message = Console.ReadLine();
				var body = Encoding.UTF8.GetBytes(message);
				model.BasicPublish("", "myqueue", properties, body);
			}
		}
	}
}