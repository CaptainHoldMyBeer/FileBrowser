using System.Threading.Tasks;
using Windows.Storage;

namespace FileManager.Modules.Interfaces
{
	interface IOpenProvider
	{
		Task<object> Open(StorageFile file);
	}
}
