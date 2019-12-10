using System;

namespace FileManager.Modules.Exceptions
{
	public class FileException:Exception
	{
		public FileException()
		{

		}
		public FileException(string message) : base(message)
		{

		}
	}
}
