using System.Web.OData;

namespace TwitterApi
{
	public static class ODataActionParametersExtensions
	{
		public static T GetValue<T>(this ODataActionParameters parameters, string key)
		{
			object result;
			if (parameters.TryGetValue(key, out result) && result is T)
			{
				return (T)result;
			}

			return default(T);
		}
	}
}