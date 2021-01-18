using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBD.DbModels
{
    public class DailyElectricitySummary
    {
        [Key] public int Id { get; set; }
        [Required] public DateTimeOffset Date { get; set; }
        [Required] public double EnergyProduction { get; set; }
        [Required] public double EnergyImport { get; set; }
        [Required] public double EnergyExport { get; set; }
        [Required] public double EnergyProductionSum { get; set; }
        [Required] public double EnergyImportSum { get; set; }
        [Required] public double EnergyExportSum { get; set; }
        [Required] public int MaxPowerProduction { get; set; }
        [Required] public int MaxPowerImport { get; set; }
        [Required] public int MaxPowerExport { get; set; }
        [Required] public int MaxPowerConsumption { get; set; }
        [Required] public int MaxPowerUse { get; set; }
        [Required] public int MaxPowerStore { get; set; }
    }
}
