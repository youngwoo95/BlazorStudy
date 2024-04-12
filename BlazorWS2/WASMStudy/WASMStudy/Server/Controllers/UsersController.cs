using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WASMStudy.Server.Services;
using WASMStudy.Server.Services.Interfaces;
using WASMStudy.Shared;

namespace WASMStudy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService IUserService;

        public UsersController(IUsersService _iuserservice)
        {
            IUserService = _iuserservice;
        }

        [HttpGet]

        public async Task<IEnumerable<Userinfo>> Get()
        {
            return await IUserService.GetAllAsync();
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Post([FromBody]Userinfo model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            try
            {
                await this.IUserService.AddAsync(model);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Update([FromBody] Userinfo info)
        {
            Userinfo model = await IUserService.GetByIdAsync(info.Userid);

            if(model == null)
            {
                return NotFound();
            }

            try
            {
                await this.IUserService.EditAsync(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }


    }
}
