using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using FileManager.Modules.Interfaces;


namespace FileManager.Models
{
	public class Cache: ICacheProvider
	{
		private readonly int _maxCapacity = 3;
		private List<StorageFile> _cache;

		public async void Add(IHistoryProvider _historyProvider)
		{
			_cache = new List<StorageFile>(_maxCapacity);

			int currentIndex = _historyProvider.Index;

			if (_historyProvider.Index == _historyProvider.Count - 1)
			{
				_cache.Add(_historyProvider.PreviousFile().Result);
				_cache.Add(_historyProvider.NextFile().Result);
			}

			if (_historyProvider.Index == 0)
			{
				_cache.Add(_historyProvider.CurrentFile().Result);
				_cache.Add(_historyProvider.NextFile().Result);
			}
			else
			{
				_cache.Add(_historyProvider.PreviousFile().Result);
				_cache.Add(_historyProvider.NextFile().Result);
				_cache.Add(_historyProvider.NextFile().Result);
			}

			_historyProvider.Index = currentIndex;
		}

		public	async Task<StorageFile> NextFile()
		{
			return _cache[_cache.Count-1];
		}

		public async  Task<StorageFile> PreviousFile()
		{
			return _cache[0];
		}
	}
}
