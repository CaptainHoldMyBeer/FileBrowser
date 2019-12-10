using System.Threading.Tasks;
using Windows.Storage;

namespace FileManager.Modules.Interfaces
{
	interface ICacheProvider
	{
		void Add(IHistoryProvider provider);
		Task<StorageFile> NextFile();
		Task<StorageFile> PreviousFile();
	}
}
