using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using ImageResizer;

namespace ImagingService.Helpers
{
	public static class ImageHelper
	{
		private static readonly string imagesFolder = HostingEnvironment.MapPath("~/Images");
		
		public static void StoreImage(string name, byte[] image)
		{
			var filename = Path.GetFileNameWithoutExtension(name);
			var extension = Path.GetExtension(name).Trim('.');
			var sizes = Enum.GetValues(typeof(Size)).Cast<Size>();

			var folder = GetFolderName(filename);
			if (Directory.Exists(folder))
			{
				Directory.Delete(folder, true);
			}

			Directory.CreateDirectory(folder);
			foreach (var size in sizes)
			{
				var resized = Resize(size, image, extension);
				SaveToDisk(folder, size, resized, extension);
			}
		}

		public static byte[] GetImage(string name, int preferredSize)
		{
			return new byte[0];
		}

		private static byte[] Resize(Size size, byte[] image, string extension)
		{
			var settings = new ResizeSettings
			{
				MaxWidth = (int)size,
				MaxHeight = (int)size,
				Format = extension
			};
			using (var sourceStream = new MemoryStream(image))
			using (var destinationStream = new MemoryStream())
			{
				ImageBuilder.Current.Build(sourceStream, destinationStream, settings);
				return destinationStream.ToArray();
            }
		}

		private static void SaveToDisk(string folder, Size size, byte[] data, string extension)
		{
			var destinationPath = Path.Combine(folder, size + "." + extension);
			File.WriteAllBytes(destinationPath, data);
		}

		private static string GetFolderName(string imageName)
		{
			return Path.Combine(imagesFolder, imageName);
		}

		private enum Size
		{
			Max200 = 200,
			Max500 = 500,
			Max1000 = 1000,
			Max2000 = 2000,
			Max3200 = 3200,
		}
	}
}