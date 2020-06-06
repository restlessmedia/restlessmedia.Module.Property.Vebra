using System.IO;

namespace restlessmedia.Module.Property.Vebra
{
  public interface IApiPropertyProvider : IProvider
  {
    Stream GetStream();
  }
}