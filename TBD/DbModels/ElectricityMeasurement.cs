using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD.DbModels
{
    public class ElectricityMeasurement
    {
        [Key] public int Id { get; set; }
        [Required] [Column("Timestamp")] public DateTimeOffset DateTime { get; set; }
        [Required] public double EnergyProduction { get; set; }
        [Required] public double EnergyImport { get; set; }
        [Required] public double EnergyExport { get; set; }
        [Required] public int PowerProduction { get; set; }
        [Required] public int PowerImport { get; set; }
        [Required] public int PowerExport { get; set; }
    }
}
