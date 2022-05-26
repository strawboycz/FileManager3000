using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
								directoryPath = ReadValue("Directory", @"C:/Windows/System32/");
								if (!Directory.Exists(directoryPath) && directoryPath == null)
								{
										Console.WriteLine($"Directory {directoryPath} does not exist");
								}
						} while (!Directory.Exists(directoryPath) && directoryPath == null);

						var selectedExtensions = ReadValue("File extensions", "All extesions");
						if (selectedExtensions == "All extensions")
						{
							selectedExtensions = null;

						}
						var extensions = selectedExtensions.Split(';');


						extensions.Select(x => x.Trim()).ToList();


						var files = Directory.GetFiles(directoryPath);
						var fileInfos = files.Select(x => new FileInfo(x)).ToList();


						var totalSize = fileInfos.Sum(x => x.Length);
						var avgSize = fileInfos.Average(x => x.Length);
						Console.WriteLine($"Total size: {totalSize / 1024 / 1024}MB, average size: {avgSize / 1024 / 1024}MB");
						var onlySelectedExtensionFilenames = fileInfos.Where(x => x.Extension == $"{extensions}")
								.Select(y => y.FullName).ToList();

						var onlySelectedExtensionsSum = onlySelectedExtensionFilenames.Select(x => new FileInfo(x)).Sum(x => x.Length);

						onlySelectedExtensionFilenames.ForEach(x => Console.WriteLine(x));

						Console.WriteLine($"Total size of selected files is: {onlySelectedExtensionsSum}");



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
