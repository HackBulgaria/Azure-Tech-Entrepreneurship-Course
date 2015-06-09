using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ChatBackend.Helpers
{
	public static class RedisHelper
	{
		private const string connectionString = "TempCache.redis.cache.windows.net,ssl=true,password=1bMKUEzI0UEj6m6/QJ0T1rhs2afjEvmxW0xB3F0KQQU=";
		private static readonly Lazy<ConnectionMultiplexer> redis = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));

		public static IEnumerable<T> GetCollection<T>(string key)
		{
			var db = redis.Value.GetDatabase();
			return db.ListRange(key).Select(v => JsonConvert.DeserializeObject<T>(v));
		}

		public static void AddToCollection(string key, object value)
		{
			var db = redis.Value.GetDatabase();
			db.ListRightPush(key, JsonConvert.SerializeObject(value));
		}

		public static void CleanUp()
		{
			var db = redis.Value.GetDatabase();
			var server = redis.Value.GetServer(redis.Value.GetEndPoints().First());
			foreach (var key in server.Keys())
			{
				db.KeyDelete(key);
			}
		}
	}
}