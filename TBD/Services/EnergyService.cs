using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TBD.Core;
using TBD.DbModels;
using TBD.Interfaces;
using TBD.Models;

namespace TBD.Services
{
    public class EnergyService : IEnergyService
    {
        private readonly IMeasurementFetcher _measurementFetcher;
        private readonly TBDDbContext _context;
        private readonly AppSettings _appSettings;

        public EnergyService(IMeasurementFetcher measurementFetcher, TBDDbContext context, IOptions<AppSettings> appSettings)
        {
            _measurementFetcher = measurementFetcher;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<EnergyIndexViewModel> GetEnergyIndexViewModel()
        {
            var energyIndexViewModelTask = GetPowerIndexViewModel();
            var dayStart = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                0, 0, 0, DateTimeOffset.Now.Offset);

            var energySummaryTodayTask = GetEnergySummary(dayStart, DateTimeOffset.Now);
            var energySummaryAllTask = GetEnergySummary(DateTimeOffset.MinValue, DateTimeOffset.Now);

            var energyIndexViewModel = await energyIndexViewModelTask;
            var energySummaryToday = await energySummaryTodayTask;
            var energySummaryAll = await energySummaryAllTask;

            energyIndexViewModel.EnergySummaryToday = energySummaryToday;
            energyIndexViewModel.EnergySummaryAll = energySummaryAll;

            return energyIndexViewModel;
        }

        public async Task<EnergyIndexViewModel> GetPowerIndexViewModel()
        {
            var electricityMeasurement = await _measurementFetcher.GetElectricityMeasurement();

            return new EnergyIndexViewModel
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

        public async Task<EnergySummary> GetEnergySummary(DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            var fromMeasurementTask = _context.ElectricityMeasurement
                .Where(x => x.DateTime >= fromDate && x.DateTime <toDate)
                .FirstOrDefaultAsync();

            var toMeasurementTask = _context.ElectricityMeasurement
                .Where(x => x.DateTime > fromDate && x.DateTime <= toDate)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            var fromMeasurement = await fromMeasurementTask;
            var toMeasurement = await toMeasurementTask;

            toMeasurement ??= new ElectricityMeasurement();
            fromMeasurement ??= new ElectricityMeasurement();

            if (fromMeasurement.Id == 1)
                fromMeasurement = new ElectricityMeasurement();

            var energySummary = new EnergySummary
            {
                Production = toMeasurement.EnergyProduction - fromMeasurement.EnergyProduction,
                Import = toMeasurement.EnergyImport - fromMeasurement.EnergyImport,
                Export = toMeasurement.EnergyExport - fromMeasurement.EnergyExport
            };

            energySummary.Use = energySummary.Production - energySummary.Export;
            energySummary.Consumption = energySummary.Use + energySummary.Import;
            energySummary.Store = energySummary.Export * _appSettings.EnergyReturnFactor - energySummary.Import;

            return energySummary;
        }

        private double GetPowerPercentage(int power)
        {
            return Math.Round(Math.Min(Math.Abs(power) / 40.0, 100), 1);
        }
    }
}
