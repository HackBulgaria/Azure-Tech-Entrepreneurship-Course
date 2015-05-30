using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.Web.Http.Dependencies;

namespace TwitterApi.Composition
{
	public class StandaloneDependencyScope : IDependencyScope
	{
		private readonly Export<CompositionContext> compositionScope;

		protected CompositionContext CompositionScope
		{
			get
			{
				return this.compositionScope.Value;
			}
		}

		public StandaloneDependencyScope(Export<CompositionContext> compositionScope)
		{
			if (compositionScope == null)
			{
				throw new ArgumentNullException(nameof(compositionScope));
			}

			this.compositionScope = compositionScope;
		}

		public void Dispose()
		{
			this.compositionScope.Dispose();
		}

		public object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw new ArgumentNullException(nameof(serviceType));
			}

			object result;
			this.CompositionScope.TryGetExport(serviceType, out result);
			return result;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			if (serviceType == null)
			{
				throw new ArgumentNullException(nameof(serviceType));
			}

			return this.CompositionScope.GetExports(serviceType);
		}
	}
	public class StandaloneDependencyResolver : StandaloneDependencyScope, IDependencyResolver
	{
		private readonly ExportFactory<CompositionContext> requestScopeFactory;

		public StandaloneDependencyResolver(CompositionHost rootCompositionScope) : base(new Export<CompositionContext>(rootCompositionScope, rootCompositionScope.Dispose))
		{
			if (rootCompositionScope == null)
			{
				throw new ArgumentNullException(nameof(rootCompositionScope));
			}

			var metadataConstraints = new Dictionary<string, object>
			{
				{ "SharingBoundaryNames", new[] { "HttpRequest" } }
			};

			var factoryContract = new CompositionContract(typeof(ExportFactory<CompositionContext>), contractName: null, metadataConstraints: metadataConstraints);

			this.requestScopeFactory = (ExportFactory<CompositionContext>)rootCompositionScope.GetExport(factoryContract);
		}

		public IDependencyScope BeginScope()
		{
			return new StandaloneDependencyScope(this.requestScopeFactory.CreateExport());
		}
	}
}