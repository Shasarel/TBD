using System;
using System.Reflection.Metadata.Ecma335;
using TBD.DbModels;

namespace TBD.Models
{
    public class EnergyNowViewModel
    {
        public ElectricityMeasurement ElectricityMeasurement { get; set; }
        public double PowerProductionPercentage { get; set; }
        public double PowerConsumptionPercentage { get; set; }
        public double PowerImportPercentage { get; set; }
        public double PowerExportPercentage { get; set; }
        public double PowerUsePercentage { get; set; }
        public double PowerStorePercentage { get; set; }
    }
}
