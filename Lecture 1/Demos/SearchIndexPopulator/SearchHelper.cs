using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace SearchIndexPopulator
{
	public static class SearchHelper<T> where T : new()
	{
		private const string ServiceName = "companysearch";
		private const string ApiKey = "052EA476C22F48756CCD743880A07000";
		private static readonly SearchServiceClient searchClient = new SearchServiceClient(ServiceName, new SearchCredentials(ApiKey));
		private static readonly string indexName = typeof(T).Name.ToLower(); // Index name must be lowercase
		private static readonly IEnumerable<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

		public static void CreateIndex()
		{
			var index = new Index
			{
				Name = indexName,
				Fields = properties.Select(pi => new
										   {
											   Name = pi.Name,
											   DataType = typeof(DataType).GetField(pi.PropertyType.Name)?.GetValue(null) as DataType,
											   IsKey = pi.Name == "Id"
										   })
								   .Where(p => p.DataType != null)
								   .Select(p => new Field(p.Name, p.DataType) { IsKey = p.IsKey, IsSearchable = !p.IsKey })
								   .ToArray()
			};

			searchClient.Indexes.CreateOrUpdate(index);
		}

		public static void PopulateData(IEnumerable<T> values)
		{
			var actions = values.Select(v => new IndexAction(IndexActionType.MergeOrUpload, Convert(v)));
			var batch = new IndexBatch(actions);
			searchClient.Indexes.GetClient(indexName).Documents.Index(batch);
		}

		public static IEnumerable<T> Search(string query)
		{
			var results = searchClient.Indexes.GetClient(indexName).Documents.Search(query);
			return results.Select(r => Convert(r.Document));
		}

		private static T Convert(Document doc)
		{
			var result = new T();
			foreach (var kvp in doc)
			{
				var pi = properties.FirstOrDefault(p => p.Name == kvp.Key);
				if (pi != null)
				{
					pi.SetValue(result, kvp.Value);
				}
			}

			return result;
		}

		private static Document Convert(T value)
		{
			var result = new Document();
			foreach (var pi in properties)
			{
				result[pi.Name] = pi.GetValue(value);
			}

			return result;
		}
	}
}
