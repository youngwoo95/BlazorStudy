using BlazorAppTestWasm.Server.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppTestWasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GanamguPopulationController : ControllerBase
    {
        private readonly IGangnamguPopulation _IGangnamguPopulation;

        public GanamguPopulationController(IGangnamguPopulation iGangnamguPopulation)
        {
            _IGangnamguPopulation = iGangnamguPopulation;
        }

        [HttpGet]
        public IEnumerable<GanamguPopulation> Get()
        {
            return _IGangnamguPopulation.GetAllGanamguPopulation();
        }




    }

}
