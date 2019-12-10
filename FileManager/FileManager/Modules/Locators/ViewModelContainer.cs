using Autofac;
using FileManager.Models;
using FileManager.Modules.Interfaces;
using FileManager.ViewModels;

namespace FileManager.Modules.Locators
{
	public class ViewModelContainer
	{
		private static readonly IContainer _container;

		public static MainPageViewModel MainPageInstance =>
			_container.Resolve<MainPageViewModel>();

		public ViewModelContainer()
		{

		}
		static ViewModelContainer()
		{
			ContainerBuilder builder = new ContainerBuilder();

			ViewModelContainer.AddBindings(builder);

			_container = builder.Build();
		}

		public static IContainer GetContainer()
		{
			return _container;
		}

		private static void AddBindings(ContainerBuilder builder)
		{
			builder.RegisterType<File>().As<INavigateProvider>();

			builder.RegisterType<Cache>().As<ICacheProvider>();

			builder.RegisterType<File>().As<IOpenProvider>();

			builder.RegisterType<History>().As<IHistoryProvider>();

			builder.RegisterType<Loger.Loger>().As<ILoger>();

			builder.RegisterType<MainPageViewModel>();
		}
	}
}
