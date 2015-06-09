using System;

namespace MyNativeApp
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ExportToDIAttribute : Attribute
	{
		public Type Type { get; private set; }

		public ExportToDIAttribute (Type type = null)
		{
			this.Type = type;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class ImportAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class ImportManyAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class SharedAttribute : Attribute
	{
	}
}