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
								directoryPath = ReadValue("Directory", "C:\\Windows\\System32");
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

						extensions.Select(x => x.Trim()).ToList();


						var files = Directory.GetFiles(directoryPath);
						var fileInfos = files.Select(x => new FileInfo(x)).ToList();
						//C:\Users\Mirek\Documents
						List<FileInfo> selectedExtensionFilenames = new List<FileInfo>();
						if (selectedExtensions != "")
						{
								extensions.ForEach(extension =>
									selectedExtensionFilenames.AddRange(fileInfos.Where(x => x.Extension == $"{extension}")
										.ToList()));
						}
						else
						{
							selectedExtensionFilenames.AddRange(fileInfos.ToList());
						}


						var selectedExtensionsSum = selectedExtensionFilenames.Sum(x => x.Length);
						var selectedExtensionsAvg = selectedExtensionFilenames.Average(x => x.Length);

						selectedExtensionFilenames.ForEach(x => Console.WriteLine(x));

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
