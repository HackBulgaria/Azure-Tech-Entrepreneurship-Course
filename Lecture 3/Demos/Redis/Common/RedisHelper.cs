using System;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Common
{
	public static class RedisHelper
    {
		private const string connectionString = "TempCache.redis.cache.windows.net,ssl=true,password=1bMKUEzI0UEj6m6/QJ0T1rhs2afjEvmxW0xB3F0KQQU=";
		private static Lazy<ConnectionMultiplexer> redis = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));

		public static void Publish(string channel, object value)
		{
			var sub = redis.Value.GetSubscriber();
			sub.Publish(channel, JsonConvert.SerializeObject(value));
		}

		public static void Subscribe<T>(string channel, Action<T> callback)
		{
			var sub = redis.Value.GetSubscriber();
			sub.Subscribe(channel, (originalChannel, value) =>
			{
				callback(JsonConvert.DeserializeObject<T>(value));
			});
		}

		// Not used in the demo - here to demonstrate the database aspect of Redis
		public static void Set(string key, object value)
		{
			var db = redis.Value.GetDatabase();
			db.StringSet(key, JsonConvert.SerializeObject(value));
		}

		public static T Get<T>(string key)
		{
			var db = redis.Value.GetDatabase();
			var value = db.StringGet(key);
			return JsonConvert.DeserializeObject<T>(value);
		}
	}
}