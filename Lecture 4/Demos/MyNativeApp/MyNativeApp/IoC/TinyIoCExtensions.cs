using System.Reflection;
using System.Linq;
using TinyIoC;
using System.Collections.Generic;
using System;

namespace MyNativeApp
{
	public static class TinyIoCExtensions
	{
		public static void RegisterExports(this Assembly assembly)
		{
			var types = assembly.GetTypes()
								.Select(t => new 
											{ 
												Type = t, 
												ExportAttribute = t.GetCustomAttribute<ExportToDIAttribute>(),
												IsShared = t.GetCustomAttribute<SharedAttribute>() != null
											})
								.Where(t => t.ExportAttribute != null)
								.GroupBy(t => t.ExportAttribute.Type);

			foreach (var typeInfoGroup in types)
			{
				if (typeInfoGroup.Key != null && typeInfoGroup.Count() > 1)
				{
					TinyIoCContainer.Current.RegisterMultiple(typeInfoGroup.Key, typeInfoGroup.Select(t => t.Type));
				}
				else
				{
					foreach (var export in typeInfoGroup)
					{
						TinyIoCContainer.RegisterOptions options;
						if (export.ExportAttribute.Type != null)
						{
							options = TinyIoCContainer.Current.Register(typeInfoGroup.Key, export.Type);
						}
						else
						{
							options = TinyIoCContainer.Current.Register(export.Type);
						}

						if (export.IsShared)
						{
							options.AsSingleton();
						}
					}
				}
			}
		}
	
		public static void ResolveDependencies(this object instance)
		{
			var imports = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
											.Where(pi => pi.GetCustomAttribute<ImportAttribute>() != null &&
				                                         !pi.PropertyType.IsValueType &&
				                                         pi.GetValue(instance) == null);

			foreach (var import in imports)
			{
				var value = TinyIoCContainer.Current.Resolve(import.PropertyType);
				import.SetValue(instance, value);
			}
		}
	}
}