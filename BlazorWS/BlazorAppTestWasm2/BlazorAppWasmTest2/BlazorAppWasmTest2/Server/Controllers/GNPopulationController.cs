using BlazorAppWasmTest2.Server.Interfaces;
using BlazorAppWasmTest2.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppWasmTest2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GNPopulationController : ControllerBase
    {
        private readonly IGNPopulation IGNPopulation;

        public GNPopulationController(IGNPopulation _IGNPopulation)
        {
            this.IGNPopulation = _IGNPopulation;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IGNPopulation.DeleteGNPopulation(id);
        }
    }
}
