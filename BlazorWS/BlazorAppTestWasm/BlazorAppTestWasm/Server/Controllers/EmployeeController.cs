using BlazorAppTestWasm.Server.Models;
using BlazorAppTestWasm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppTestWasm.Server.Controllers
{
    [Route("api/[controller]")] // ==> api/Employee 가 URI가 되는것임.
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private InfrunContext context = new InfrunContext();

        [HttpGet]
        [Route("read")]
        public IEnumerable<Employee> GetEmployees()
        {
            return this.context.Employees.ToList();
        }


        [HttpPost]
        [Route("insert")] //템플릿 파라미터에 특정 string을 지정할 수 있음. - 여러개가 있을 경우를 대비해서 있는 옵션.
        public async Task<IActionResult> Post([FromBody]Employee model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            // DB INSERT
            this.context.Add(model);
            try
            {
                await this.context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Employee model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            // DB UPDATE
            this.context.Update(model);

            try
            {
                await this.context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await context.Employees.FindAsync(id);
            
            if(model == null)
            {
                return NotFound();
            }

            this.context.Employees.Remove(model);

            try
            {
                await this.context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
