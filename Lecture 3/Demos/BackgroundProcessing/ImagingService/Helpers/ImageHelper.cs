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
			var folder = GetFolderName(name);
			if (Directory.Exists(folder))
			{
				Directory.Delete(folder, true);
			}

			Directory.CreateDirectory(folder);
			var sizes = Enum.GetValues(typeof(Size)).Cast<Size>();
			foreach (var size in sizes)
			{
				var resized = Resize(size, image);
				SaveToDisk(folder, size, resized);
			}
		}

		public static Stream GetImage(string name, int preferredSize)
		{
			var folder = GetFolderName(name);
			var sizes = Directory.EnumerateFiles(folder)
								 .Select(f => new { Size = int.Parse(Path.GetFileNameWithoutExtension(f)), Path = f })
								 .OrderByDescending(f => f.Size)
								 .ToArray();

			if (sizes.Any())
			{
				var path = sizes.Where(s => s.Size >= preferredSize)
								.OrderBy(s => s.Size)
								.Select(f => f.Path)
								.FirstOrDefault();

				if (path == null)
				{
					path = sizes.First().Path;
				}

				return File.OpenRead(path);
			}
			else
			{
				return Stream.Null;
			}
		}

		private static byte[] Resize(Size size, byte[] image)
		{
			var settings = new ResizeSettings
			{
				MaxWidth = (int)size,
				MaxHeight = (int)size,
				Format = "jpg"
			};
			using (var sourceStream = new MemoryStream(image))
			using (var destinationStream = new MemoryStream())
			{
				ImageBuilder.Current.Build(sourceStream, destinationStream, settings);
				return destinationStream.ToArray();
            }
		}

		private static void SaveToDisk(string folder, Size size, byte[] data)
		{
			var destinationPath = Path.Combine(folder, ((int)size) + ".jpg");
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