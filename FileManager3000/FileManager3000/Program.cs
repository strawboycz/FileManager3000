using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

						List<ExtensionSpecs> specsOfExtensions = new List<ExtensionSpecs>();

						extensions.ForEach(extension => specsOfExtensions.Add(new ExtensionSpecs(extension)));

						string[] files;
						if (directoryPath != "")
						{
							files = Directory.GetFiles(directoryPath);
						}
						else
						{
							files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.System));
						}
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

						specsOfExtensions.ForEach(spec => selectedExtensionFiles.Where(x => x.Extension == spec.Name).ToList().ForEach(file => spec.IncrementCount()));
						specsOfExtensions.ForEach(spec => selectedExtensionFiles.Where(x => x.Extension == spec.Name).ToList().ForEach(file => spec.AddToSize((int)file.Length)));

						var selectedExtensionsFilesSum = selectedExtensionFiles.Sum(x => x.Length);
						var selectedExtensionsFilesCount = selectedExtensionFiles.Count;

						selectedExtensionFiles.ForEach(x => Console.WriteLine(x));
						Console.WriteLine();
						specsOfExtensions.ForEach(spec => Console.WriteLine($"There is {spec.Count} files with {spec.Name} extension with total size of {spec.TotalSize /1024 /1024}MB"));


						Console.WriteLine($"There is {selectedExtensionsFilesCount} total files and their size is: {selectedExtensionsFilesSum / 1024 / 1024}MB");



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
