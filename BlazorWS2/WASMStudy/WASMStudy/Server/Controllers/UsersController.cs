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

        public async ValueTask<IEnumerable<Userinfo>> Get()
        {
            return await IUserService.GetAllAsync();
        }

        [HttpGet]
        [Route("select/{id:int}")]
        public async ValueTask<Userinfo> Select(int id)
        {
            return await IUserService.GetByReqNoAsync(id);
        }

        [HttpPost]
        [Route("insert")]
        public async ValueTask<IActionResult> Post([FromBody]Userinfo model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            try
            {
                await this.IUserService.AddAsync(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("delete/{userid}")]
        public async ValueTask<IActionResult> Delete(string userid)
        {
            Userinfo model = await IUserService.GetByIdAsync(userid);
           
            if (model == null)
            {
                return BadRequest();
            }

            try
            {
                await this.IUserService.DeleteAsync(model.Reqno);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPut]
        [Route("edit")]
        public async ValueTask<IActionResult> Update([FromBody] Userinfo info)
        {

            if(info == null)
            {
                return NotFound();
            }

            try
            {
                await this.IUserService.EditAsync(info);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }


    }
}
