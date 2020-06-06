using System;

namespace restlessmedia.Module.Property.Vebra
{
  public interface ISync
  {
    DateTime? LastAccessed { get; set; }

    string ApplicationName { get; set; }

    bool IsRunning { get; set; }
  }
}
