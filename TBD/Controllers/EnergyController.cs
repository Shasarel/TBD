using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBD.Core.Authorization;
using TBD.Interfaces;

namespace TBD.Controllers
{
    public class EnergyController : TBDController
    {
        private readonly IEnergyService _energyService;
        public EnergyController(IEnergyService energyService)
        {
            _energyService = energyService;
        }

        [HttpGet]
        public async Task<JsonResult> GetPowerNow()
        {
            return Json(await _energyService.GetPowerIndexViewModel());
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _energyService.GetEnergyIndexViewModel());
        }
    }
}
