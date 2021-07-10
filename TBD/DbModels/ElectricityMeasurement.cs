using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TBD.DbModels
{
    public class ElectricityMeasurement
    {
        [Key] public int Id { get; set; }
        [Required] [Column("Timestamp")] public DateTimeOffset DateTime { get; set; } = DateTimeOffset.UtcNow;
        [Required] public double EnergyProduction { get; set; } = 0;
        [Required] public double EnergyImport { get; set; } = 0;
        [Required] public double EnergyExport { get; set; } = 0;
        [Required] public int PowerProduction { get; set; } = 0;
        [Required] public int PowerImport { get; set; } = 0;
        [Required] public int PowerExport { get; set; } = 0;
        [NotMapped] public int PowerConsumption { get; set; } = 0;
        [NotMapped] public int PowerUse { get; set; } = 0;
        [NotMapped] public int PowerStore { get; set; } = 0;
        [NotMapped] public bool Correct { get; set; } = false;
    }
}
