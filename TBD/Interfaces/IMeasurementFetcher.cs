using System;
using System.Threading.Tasks;
using TBD.DbModels;

namespace TBD.Interfaces
{
    public interface IMeasurementFetcher
    {
        public Task<MeteoMeasurement> GetMeteoMeasurement();
        public Task<ElectricityMeasurement> GetElectricityMeasurement();
        public Task<Tuple<ElectricityMeasurement, MeteoMeasurement>> GetAllMeasurements();
    }
}
