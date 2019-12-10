using System;
using System.IO;
using System.Threading.Tasks;
using FileManager.Modules.Interfaces;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Newtonsoft.Json.Linq;


namespace FileManager.Models
{
	public class File:INavigateProvider,IOpenProvider
	{
		private StorageFile _file;

		public File()
		{
			
		}
		public async Task<StorageFile> NavigateFile()
		{
			FileOpenPicker filePicker = new FileOpenPicker();
			filePicker.ViewMode = PickerViewMode.Thumbnail;
			filePicker.FileTypeFilter.Add(".txt");
			filePicker.FileTypeFilter.Add(".json");
			filePicker.FileTypeFilter.Add(".png");
			filePicker.FileTypeFilter.Add(".jpg");
			filePicker.FileTypeFilter.Add(".jpeg");
			
			_file = await filePicker.PickSingleFileAsync();

			return _file;
		}

		public async Task<IRandomAccessStream> GetFile()
		{
			if (_file != null)
			{
				return await _file.OpenAsync(FileAccessMode.Read);
			}
			
			return null;
		}

		public async Task<object> Open(StorageFile file)
		{
			if (GetType(file)==FileType.Image)
			{
				var image = new BitmapImage();
				IRandomAccessStream stream = file.OpenStreamForReadAsync().Result.AsRandomAccessStream();
				image.SetSource(stream);
				stream.Dispose();
				return image;
			}

			if (GetType(file)==FileType.JSON)
			{
				var text = await FileIO.ReadTextAsync(file);
				return JObject.Parse(text);
			}

			if (GetType(file)==FileType.Text)
			{
				return await FileIO.ReadTextAsync(file);
			}
			return null;
		}

		private FileType GetType(StorageFile file)
		{
			if (file.ContentType.Contains("image"))
				return FileType.Image;
			if (file.ContentType.Contains("json"))
				return FileType.JSON;
			if (file.ContentType.Contains("text"))
				return FileType.Text;
			return FileType.Undefined;
		}
	}
}
