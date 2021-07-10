using Microsoft.EntityFrameworkCore;
using TBD.DbModels;
using TBD.Interfaces;

namespace TBD
{
    public class TBDDbContext : DbContext
    {
        private readonly IValueConverterService _dbValueConverter;
        public TBDDbContext(DbContextOptions<TBDDbContext> options, IValueConverterService dbValueConverter) : base(options)
        {
            _dbValueConverter = dbValueConverter;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BlindTask>()
                .Property(e => e.DateTime)
                .HasConversion(_dbValueConverter.DateTimeOffsetTimestampConverter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.DateTime)
                .HasConversion(_dbValueConverter.DateTimeOffsetTimestampConverter);

            modelBuilder
                .Entity<MeteoMeasurement>()
                .Property(e => e.DateTime)
                .HasConversion(_dbValueConverter.DateTimeOffsetTimestampConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.Date)
                .HasConversion(_dbValueConverter.DateTimeOffsetDateIntConverter);

            modelBuilder
                .Entity<EnergyCorrection>()
                .Property(e => e.Date)
                .HasConversion(_dbValueConverter.DateTimeOffsetDateIntConverter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.Date)
                .HasConversion(_dbValueConverter.DateTimeOffsetDateIntConverter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyProduction)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyExport)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyImport)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyProductionSum)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyImportSum)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);
            modelBuilder
                .Entity<DailyElectricitySummary>()
                .Property(e => e.EnergyExportSum)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.EnergyProduction)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.EnergyImport)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<ElectricityMeasurement>()
                .Property(e => e.EnergyExport)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<EnergyCorrection>()
                .Property(e => e.Correction)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<MeteoMeasurement>()
                .Property(e => e.Temperature)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<MeteoMeasurement>()
                .Property(e => e.Pressure)
                .HasConversion(_dbValueConverter.IntDouble100Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.TemperatureMin)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.TemperatureAvg)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.TemperatureMax)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.HumidityMin)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.HumidityAvg)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.HumidityMax)
                .HasConversion(_dbValueConverter.IntDouble1000Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.PressureMin)
                .HasConversion(_dbValueConverter.IntDouble100Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.PressureAvg)
                .HasConversion(_dbValueConverter.IntDouble100Converter);

            modelBuilder
                .Entity<DailyMeteoSummary>()
                .Property(e => e.PressureMax)
                .HasConversion(_dbValueConverter.IntDouble100Converter);
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
