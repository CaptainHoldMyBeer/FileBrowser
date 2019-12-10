using Windows.Storage;
using FileManager.Modules.Interfaces;
using XLog;
using XLog.Formatters;
using XLog.NET.Targets;

namespace FileManager.Modules.Loger
{
	public class Loger:ILoger
	{
		private static Logger _logger;
		public  Loger()
		{
			var formatter = new LineFormatter();
			var logConfig = new LogConfig(formatter);

			var target = new SyncFileTarget(System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, @"FileManagerLogs.log"));
			
			logConfig.AddTarget(LogLevel.Trace, LogLevel.Fatal, target);
			LogManager.Init(logConfig);
			_logger = LogManager.Default.GetLogger("FileManager logs");
		}

		public void WriteInfo(string message)
		{
			_logger.Info(message);
			LogManager.Default.Flush();
		}

		public void WriteException(string message)
		{
			_logger.Error(message);
			LogManager.Default.Flush();
		}
	}
}
