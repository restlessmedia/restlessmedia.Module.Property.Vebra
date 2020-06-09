using restlessmedia.Module.Property.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Property.Vebra.Data
{
  [Table("VApiProperty")]
  internal class VApiProperty
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("PropertyId")]
    public long ApiPropertyId { get; set; }

    public DateTime LastChangedDate { get; set; }

    [Column("PropertyEntityId")]
    public int? PropertyId { get; set; }

    [ForeignKey("PropertyId")]
    public VProperty Property { get; set; }
  }
}