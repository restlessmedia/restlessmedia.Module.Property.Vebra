using Autofac;
using restlessmedia.Module.File;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property.Configuration;
using restlessmedia.Module.Property.Vebra.Data;

namespace restlessmedia.Module.Property.Vebra
{
  public class Module : IModule
  {
    public void RegisterComponents(ContainerBuilder containerBuilder)
    {
      containerBuilder.RegisterType<ApiPropertyService>().As<IApiPropertyService>().SingleInstance();
      containerBuilder.RegisterType<ApiPropertyDataProvider>().As<IApiPropertyDataProvider>().SingleInstance(); 

      //containerBuilder.RegisterType<FileSystemStorageProvider>().As<IDiskStorageProvider>().SingleInstance();
      //containerBuilder.RegisterType<PropertyService>().As<IPropertyService>().SingleInstance();
      //containerBuilder.RegisterType<FileDataProvider>().As<IFileDataProvider>().SingleInstance();
    }
  }
}