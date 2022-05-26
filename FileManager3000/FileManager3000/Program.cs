using System;
using System.IO;
using System.Linq;
using System.Threading.Channels;
using Microsoft.VisualBasic;

namespace FileManager3000
{
		internal class Program
		{
				static void Main(string[] args)
				{
						Console.WriteLine("FILE MANAGER 3000");
						string directoryPath;
						do
						{
							directoryPath = ReadValue("Directory", "C:\\Windows\\System32");
							if (!Directory.Exists(directoryPath))
							{
								Console.WriteLine($"Directory {directoryPath} does not exist");
							}
						} while (!Directory.Exists(directoryPath));
						

						var files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System));
						var fileInfos  = files.Select(x=> new FileInfo(x)).ToList();

						var top10Biggest = fileInfos
							.OrderByDescending(x => x.Length)
							.Take(10)
							.ToList();
						top10Biggest
							.ForEach(x=> Console.WriteLine($"File: {x.Name},Length: {x.Length / 1024 / 1024}MB"));
						var totalSize = top10Biggest.Sum(x => x.Length);
						var avgSize = top10Biggest.Average(x => x.Length);
						Console.WriteLine($"Total size: {totalSize}, average size: {avgSize}");
						var onlyPngFilenames = fileInfos.Where(x => x.Extension == ".png" && x.Length > 10_000)
								.Select(x => x.FullName).ToList();

						var onlyPngSum = onlyPngFilenames.Select(x => new FileInfo(x)).Sum(x => x.Length);

						onlyPngFilenames.ForEach(x=>Console.WriteLine(x));

						Console.WriteLine($"Total size of PNGs bigger than 50kB is: {onlyPngSum}");



				}
				public static string ReadValue(string label, string defaultVaue)
				{
						Console.Write($"{label} (Default:{defaultVaue}): ");
						string value = Console.ReadLine();
						if (value == null)
						{
								return defaultVaue;
						}
						return value;

				}
		}

}
