using System;
using System.Threading.Tasks;
using TBD.Models;

namespace TBD.Interfaces
{
    public interface IEnergyService
    {
        public Task<EnergyIndexViewModel> GetEnergyIndexViewModel();
        public Task<EnergyIndexViewModel> GetPowerIndexViewModel();

        public Task<EnergySummary> GetEnergySummary(DateTimeOffset fromDate, DateTimeOffset toDate);


    }
}
