
namespace FileManager.Modules.Interfaces
{
	interface ILoger
	{
		void WriteInfo(string message);
		void WriteException(string message);
	}
}
