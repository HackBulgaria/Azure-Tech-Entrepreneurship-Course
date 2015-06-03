using System;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqWorker
{
	class Program
	{
		static void Main(string[] args)
		{
			var factory = new ConnectionFactory
			{
				HostName = "localhost",
				UserName = "guest",
				Password = "123"
			};
			var model = factory.CreateConnection().CreateModel();
			DemoQueue(model);
		}

		// This shows fanout subscribing (like TV/Radio and the redis demo)
		static void DemoExchange(IModel model)
		{
			model.ExchangeDeclare("myexchange", "fanout");
			var queueName = model.QueueDeclare().QueueName;
			model.QueueBind(queueName, "myexchange", "");

			var consumer = new QueueingBasicConsumer(model);
			model.BasicConsume(queueName, true, consumer);

			while (true)
			{
				var message = consumer.Queue.Dequeue();
				DoWork(message);
			}
		}

		// This shows handling messages from a queue - each message is handled by a single worker
		static void DemoQueue(IModel model)
		{
			model.QueueDeclare("myqueue", true, false, false, null);
			model.BasicQos(0, 1, false);

			var consumer = new QueueingBasicConsumer(model);
			model.BasicConsume("myqueue", false, consumer);

			while (true)
			{
				var message = consumer.Queue.Dequeue();
				DoWork(message);
				// Since BasicConsume is called with noAck: false, we need to manually let Rabbit know that the message was handled
				model.BasicAck(message.DeliveryTag, false);
			}
		}

		static void DoWork(BasicDeliverEventArgs message)
		{
			var text = Encoding.UTF8.GetString(message.Body);
			Console.WriteLine("Message received: {0}", text);

			int secondsToWait;
			if (int.TryParse(text.Last().ToString(), out secondsToWait))
			{
				Console.WriteLine("Waiting {0} s", secondsToWait);
				Thread.Sleep(1000 * secondsToWait);
			}

			Console.WriteLine("Job done!");
			return message;
		}
	}
}