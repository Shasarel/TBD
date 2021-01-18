using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TBD.DbModels;

namespace TBD
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateTimeOffsetToTimestampConverter = new ValueConverter<DateTimeOffset, long>(
                v => v.ToUnixTimeSeconds(),
                v => DateTimeOffset.FromUnixTimeSeconds(v));

            var dateTimeOffsetToDateIntConverter = new ValueConverter<DateTimeOffset, int>(
                v => Convert.ToInt32($"{v.Year}{v.Month}{v.Day}"),
                v => new DateTimeOffset(
                    Convert.ToInt32(v.ToString().Substring(0, 4)),
                    Convert.ToInt32(v.ToString().Substring(4, 2)),
                    Convert.ToInt32(v.ToString().Substring(6, 2)),
                    0, 0, 0, new TimeSpan(0)));

            var WhTokWhConverter = new ValueConverter<double, int>(
                v => (int)Math.Round(v * 1000),
                v => v / 1000.0);

            var meteoDataConverter = new ValueConverter<double, int>(
                v => (int)Math.Round(v * 100),
                v => v / 100.0);


            modelBuilder
                .Entity<BlindTask>()
                .Property(e => e.DateTime)
                .HasConversion(dateTimeOffsetToTimestampConverter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.DateTime)
                .HasConversion(dateTimeOffsetToTimestampConverter);

            modelBuilder
                .Entity<MeteoMeasurement>()
                .Property(e => e.DateTime)
                .HasConversion(dateTimeOffsetToTimestampConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.Date)
                .HasConversion(dateTimeOffsetToDateIntConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.Date)
                .HasConversion(dateTimeOffsetToDateIntConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyProduction)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyExport)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyImport)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyProductionSum)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyImportSum)
                .HasConversion(WhTokWhConverter);
            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyExportSum)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.EnergyProduction)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.EnergyImport)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.EnergyExport)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<EnergyCorrection>()
                .Property(e => e.Correction)
                .HasConversion(WhTokWhConverter);

            modelBuilder
                .Entity<MeteoMeasurement>()
                .Property(e => e.Temperature)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<MeteoMeasurement>()
                .Property(e => e.Humidity)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<MeteoMeasurement>()
                .Property(e => e.Pressure)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.TemperatureMin)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.TemperatureAvg)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.TemperatureMax)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.HumidityMin)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.HumidityAvg)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.HumidityMax)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.PressureMin)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.PressureAvg)
                .HasConversion(meteoDataConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.PressureMax)
                .HasConversion(meteoDataConverter);

        }

        public DbSet<User> User { get; set; }
        public DbSet<BlindSchedule> BlindSchedule { get; set; }
        public DbSet<BlindTask> BlindTask { get; set; }
        public DbSet<DailyElectricitySummary> DailyElectricitySummary { get; set; }
        public DbSet<DailyMeteoSummary> DailyMeteoSummary { get; set; }
        public DbSet<ElectricityMeasurement> ElectricityMeasurement { get; set; }
        public DbSet<EnergyCorrection> EnergyCorrection { get; set; }
        public DbSet<MeteoMeasurement> MeteoMeasurement { get; set; }
    }
}
