using System.Threading.Tasks;
using Windows.Storage;

namespace FileManager.Modules.Interfaces
{
	public interface IHistoryProvider
	{
		int Index { get; set; }
		int Count { get; }
		void AddFile(StorageFile file);
		Task<StorageFile> NextFile();
		Task<StorageFile> CurrentFile();
		Task<StorageFile> PreviousFile();
	}
}
