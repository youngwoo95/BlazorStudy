using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WASMStudy.Server.Hubs;
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

        private readonly IHubContext<ChatHub> _hubContext;

        public UsersController(IUsersService _iuserservice, IHubContext<ChatHub> hubContext)
        {
            IUserService = _iuserservice;
            _hubContext = hubContext;
        }

        [HttpGet]

        public async ValueTask<IEnumerable<Userinfo>> Get()
        {
            
                
            return await IUserService.GetAllAsync();
        }


        [HttpGet]
        [Route("temp")]
        public async Task<IActionResult> SendMessage()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "1", "메시지");

            return Ok();
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
