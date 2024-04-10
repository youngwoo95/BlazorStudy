

using BlazorAppTestWasm.Server.Interfaces;
using BlazorAppTestWasm.Shared;
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
        [Route("read")]
        public IEnumerable<GanamguPopulation> Get()
        {
            return _IGangnamguPopulation.GetAllGanamguPopulation();
        }


        [HttpPost]
        [Route("insert")]
        public void Post(GanamguPopulation data)
        {
            _IGangnamguPopulation.AddGangnamguPopulation(data);
        }

        [HttpPut]
        [Route("put")]
        public void Put(GanamguPopulation data)
        {
            _IGangnamguPopulation.UpdateGangnamguPopulation(data);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _IGangnamguPopulation.DeleteGangnamguPopulation(id);
        }

    }

}
