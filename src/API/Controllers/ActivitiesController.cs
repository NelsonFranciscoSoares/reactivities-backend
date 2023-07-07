using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities() 
        {
            return await this.Mediator.Send(new List.Query());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id) 
        {
            return await this.Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody]Activity activity)
        {
            return Ok(await this.Mediator.Send(new Create.Command{ Activity = activity }));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditActivity([FromRoute]Guid id, [FromBody]Activity activity)
        {
            activity.Id = id;
            return Ok(await this.Mediator.Send(new Edit.Command{ Activity = activity }));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteActivity([FromRoute]Guid id)
        {
            return Ok(await this.Mediator.Send(new Delete.Command{ Id = id }));
        }
    }
}