using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Autofac;
using FileManager.Modules.Locators;
using FileManager.Modules.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json.Linq;

namespace FileManager.ViewModels
{ 
	public class MainPageViewModel:ViewModelBase
	{
		private readonly INavigateProvider _navigateProvider;
		private readonly ICacheProvider _cacheProvider;
		private readonly IOpenProvider _openProvider;
		private readonly IHistoryProvider _historyProvider;
		private readonly ILoger _logger;
		private StorageFile _file;

		private string _status;
		public string Status
		{
			get { return _status; }
			set
			{
				_status = value;
				RaisePropertyChanged();
			}
		}
		
		private string _content;
		public string Content
		{
			get { return _content; }
			set
			{
				_content = value;
				RaisePropertyChanged();
			}
		}

		private BitmapImage _image;
		public BitmapImage CurrentImage
		{
			get { return _image; }
			set
			{
				_image = value;
				RaisePropertyChanged();
			}
		}

		private string _path;
		public string Path
		{
			get { return _path; }
			set
			{
				_path = value;
				RaisePropertyChanged();
			}
		}

		private bool _textVisibility = false;
		public bool TextVisibility
		{
			get { return _textVisibility; }
			set
			{
				_textVisibility = value;
				RaisePropertyChanged();
			}
		}

		private bool _imageVisibility=false;
		public bool ImageVisibility
		{
			get { return _imageVisibility; }
			set
			{
				_imageVisibility = value;
				RaisePropertyChanged();
			}
		}

		public ICommand OpenFileCommand { get; set; }
		public ICommand NavigateFileCommand { get; set; }
		public ICommand GoRightCommand { get; set; }
		public ICommand GoLeftCommand { get; set; }

		private async void OpenFile()
		{
			await ReturnView(_file);
		}
		private async void NavigateFile()
		{
			_file = await _navigateProvider.NavigateFile();
			if (_file != null)
			{
				Path = _file.Path;
				_historyProvider.AddFile(_file);
				_logger.WriteInfo($"new file from {_file.Path} opened and added in history");
			}
			Status = _historyProvider.Index + 1 + "/" + _historyProvider.Count;

			
		}
		private async void GoRight()
		{
			if (_historyProvider.Index < 0)
			{
				var dialog = new MessageDialog("History is empty");
				await dialog.ShowAsync();
			}
			else
			{
				_cacheProvider.Add(_historyProvider);
				await ReturnView(await _cacheProvider.NextFile());
			}

			if (_historyProvider.Index < _historyProvider.Count-1)
			{
				_historyProvider.Index++;
			}

			Status = _historyProvider.Index+1 + "/" + _historyProvider.Count;
		}
		private async void GoLeft()
		{
			if (_historyProvider.Index < 0)
			{
				var dialog = new MessageDialog("History is empty");
				await dialog.ShowAsync();
			}
			else
			{
				_cacheProvider.Add(_historyProvider);
				await ReturnView(await _cacheProvider.PreviousFile());
			}
			if (_historyProvider.Index > 0)
			{
				_historyProvider.Index--;
			}
			
			Status = _historyProvider.Index+1 + "/" + _historyProvider.Count;
		}

		private async Task ReturnView(StorageFile file)
		{
			Path = file.Path;
			
			if (file.ContentType.Contains("image"))
			{
				ImageVisibility = true;
				TextVisibility = false;
				CurrentImage = (BitmapImage)(await _openProvider.Open(file));

			}

			if (file.ContentType.Contains("json"))
			{
				ImageVisibility = false;
				TextVisibility = true;
				Content = ( Task.Run(() => _openProvider.Open(file)).Result as JObject).ToString();
			}

			if (file.ContentType.Contains("text"))
			{
				ImageVisibility = false;
				TextVisibility = true;
				Content = (Task.Run(() => _openProvider.Open(file)).Result as string);
			}
		}

		public MainPageViewModel()
		{
			_cacheProvider = ViewModelContainer.GetContainer().Resolve<ICacheProvider>();

			_navigateProvider = ViewModelContainer.GetContainer().Resolve<INavigateProvider>();

			_openProvider = ViewModelContainer.GetContainer().Resolve<IOpenProvider>();

			_historyProvider = ViewModelContainer.GetContainer().Resolve<IHistoryProvider>();

			_logger = ViewModelContainer.GetContainer().Resolve<ILoger>();


			OpenFileCommand = new RelayCommand(OpenFile);

			NavigateFileCommand = new RelayCommand(NavigateFile);

			GoRightCommand = new RelayCommand(GoRight);

			GoLeftCommand = new RelayCommand(GoLeft);
		}
	}
}
