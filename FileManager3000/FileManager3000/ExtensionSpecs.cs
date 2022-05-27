namespace FileManager3000
{
	public class ExtensionSpecs
	{
		public string Name { get; private set; }
		public int Count { get; private set; } = 0;
		public int TotalSize { get; private set; } = 0;

		public ExtensionSpecs(string name)
		{
				Name = name;
		}
		public void IncrementCount()
		{
			Count++;
		}
		public void AddToSize(int size)
		{
			TotalSize += size;
		}
	}
}