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
		}

		public static void Subscribe<T>(string channel, Action<T> callback)
		{
		}
    }
}