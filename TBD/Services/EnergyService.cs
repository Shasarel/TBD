using System;
using System.Threading.Tasks;
using TBD.Interfaces;
using TBD.Models;

namespace TBD.Services
{
    public class EnergyService : IEnergyService
    {
        private readonly IMeasurementFetcher _measurementFetcher;

        public EnergyService(IMeasurementFetcher measurementFetcher)
        {
            _measurementFetcher = measurementFetcher;
        }

        public async Task<EnergyNowViewModel> GetEnergyNowViewModel()
        {
            return await GetPowerNowViewModel();
        }

        public async Task<EnergyNowViewModel> GetPowerNowViewModel()
        {
            var electricityMeasurement = await _measurementFetcher.GetElectricityMeasurement();

            return new EnergyNowViewModel
            {
                ElectricityMeasurement = electricityMeasurement,
                PowerProductionPercentage = GetPowerPercentage(electricityMeasurement.PowerProduction),
                PowerConsumptionPercentage = GetPowerPercentage(electricityMeasurement.PowerConsumption),
                PowerExportPercentage= GetPowerPercentage(electricityMeasurement.PowerExport),
                PowerImportPercentage = GetPowerPercentage(electricityMeasurement.PowerImport),
                PowerStorePercentage = GetPowerPercentage(electricityMeasurement.PowerStore),
                PowerUsePercentage = GetPowerPercentage(electricityMeasurement.PowerUse)
            };
        }

        private double GetPowerPercentage(int power)
        {
            return Math.Abs(Math.Round(Math.Min(power / 40.0, 100), 1));
        }
    }
}
