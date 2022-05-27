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
								directoryPath = ReadValue("Directory", Environment.GetFolderPath(Environment.SpecialFolder.System));
								if (!Directory.Exists(directoryPath) && directoryPath == null)
								{
										Console.WriteLine($"Directory {directoryPath} does not exist");
								}
						} while (!Directory.Exists(directoryPath) && directoryPath == null);

						var selectedExtensions = ReadValue("File extensions", "All extesions");
						if (selectedExtensions == "All extensions")
						{
								selectedExtensions = "";
						}
						List<string> extensions = selectedExtensions.Split(';').ToList();

						extensions = extensions.Select(x => x.Trim()).ToList();


						var files = Directory.GetFiles(directoryPath);
						var fileInfos = files.Select(x => new FileInfo(x)).ToList();

						List<FileInfo> selectedExtensionFiles = new List<FileInfo>();
						if (selectedExtensions != "")
						{
								extensions.ForEach(extension =>
									selectedExtensionFiles.AddRange(fileInfos.Where(x => x.Extension == $"{extension}")
										.ToList()));
						}
						else
						{
							selectedExtensionFiles.AddRange(fileInfos.ToList());
						}


						var selectedExtensionsSum = selectedExtensionFiles.Sum(x => x.Length);
						var selectedExtensionsAvg = selectedExtensionFiles.Average(x => x.Length);

						selectedExtensionFiles.ForEach(x => Console.WriteLine(x));

						Console.WriteLine($"Total size: {selectedExtensionsSum / 1024 / 1024}MB, average size: {Math.Round(selectedExtensionsAvg / 1024 / 1024)}MB");



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
