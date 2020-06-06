using System;

namespace restlessmedia.Module.Property.Vebra
{
  public class ApiPropertySync : ISync
  {
    public DateTime? LastAccessed { get; set; }

    public string ApplicationName { get; set; }

    public bool IsRunning { get; set; }
  }
}