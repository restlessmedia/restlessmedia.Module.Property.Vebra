using restlessmedia.Module.Data.EF;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Property.Vebra.Data
{
  [Table("TApiPropertyState")]
  public class TApiPropertyState
  {
    public DateTime? LastAccessed { get; set; }

    [Key, Varchar]
    public string ApplicationName { get; set; }

    public bool IsRunning { get; set; }
  }
}