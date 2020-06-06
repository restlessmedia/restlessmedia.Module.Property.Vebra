using System;

namespace restlessmedia.Module.Property.Vebra
{
  [Flags]
  public enum SyncFlags : byte
  {
    /// <summary>
    /// Flags not set or default - does nothing
    /// </summary>
    None = 0,
    /// <summary>
    /// Job has been kicked off an is currently running
    /// </summary>
    Running = 1,
    /// <summary>
    /// Recent api ran and the data was saved to the system
    /// </summary>
    DataSaved = 2,
    /// <summary>
    /// Recent api ran and all files were successfully downloaded and saved to the system
    /// </summary>
    FilesSaved = 4,
  }
}