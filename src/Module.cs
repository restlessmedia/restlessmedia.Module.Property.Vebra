using Autofac;
using restlessmedia.Module.Property.Vebra.Data;

namespace restlessmedia.Module.Property.Vebra
{
  public class Module : IModule
  {
    public void RegisterComponents(ContainerBuilder containerBuilder)
    {
      containerBuilder.RegisterType<ApiPropertyProvider>().As<IApiPropertyProvider>().SingleInstance();
      containerBuilder.RegisterType<ApiPropertyService>().As<IApiPropertyService>().SingleInstance();
      containerBuilder.RegisterType<ApiPropertyDataProvider>().As<IApiPropertyDataProvider>().SingleInstance();
    }
  }
}