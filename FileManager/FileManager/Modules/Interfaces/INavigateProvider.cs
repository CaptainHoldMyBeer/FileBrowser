using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace FileManager.Modules.Interfaces
{
	interface INavigateProvider
	{
		Task<StorageFile> NavigateFile();
		Task<IRandomAccessStream> GetFile();
	}
}
