using System;
using StackExchange.Redis;

namespace TechEntWebApp
{
	public static class RedisHelper
	{
		private const string connectionString = "MyShortener.redis.cache.windows.net,ssl=true,password=P7EQP0UvTSC8NSnbTszEcFluDi/aF1BEm4pMODmxwQY=";
		private static Lazy<ConnectionMultiplexer> redis = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));

		public static bool TryGet(string key, out string value)
		{
			value = null;
			if (key != null)
			{
				var db = redis.Value.GetDatabase();
				value = db.StringGet(key);
			}

			return value != null;
		}

		public static void Set(string key, string value)
		{
			var db = redis.Value.GetDatabase();
			db.StringSet(key, value);
		}
	}
}