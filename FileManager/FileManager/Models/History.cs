using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using FileManager.Modules.Interfaces;

namespace FileManager.Models
{
	public class History:IHistoryProvider
	{
		private List<StorageFile> _history;
		
		public int Index { get; set; } = -1;

		public int Count => _history.Count;

		public History()
		{
			_history=new List<StorageFile>();
		}

		public void AddFile(StorageFile file)
		{
			_history.Add(file);
			++Index;
		}

		public Task<StorageFile> NextFile()
		{
			if (Index < Count - 1)
			{
				return Task.Run(() => _history[++Index]);
			}

			return Task.Run(() => _history[Count-1]);
		}
		public Task<StorageFile> CurrentFile()
		{
			return Task.Run(() => _history[Index]);
		}
		public Task<StorageFile> PreviousFile()
		{
			if (Index > 0)
			{
				return Task.Run(() => _history[--Index]);
			}

			return Task.Run(() => _history[0]);
		}
	}
}
