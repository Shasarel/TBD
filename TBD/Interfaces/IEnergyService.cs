using System.Threading.Tasks;
using TBD.Models;

namespace TBD.Interfaces
{
    public interface IEnergyService
    {
        public Task<EnergyNowViewModel> GetEnergyNowViewModel();
        public Task<EnergyNowViewModel> GetPowerNowViewModel();
    }
}
