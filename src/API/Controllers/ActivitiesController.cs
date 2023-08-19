using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities() 
        {
            return HandleResult(await this.Mediator.Send(new List.Query()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id) 
        {
            var result = await this.Mediator.Send(new Details.Query{Id = id});

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody]Activity activity)
        {
            return HandleResult(await this.Mediator.Send(new Create.Command{ Activity = activity }));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> EditActivity([FromRoute]Guid id, [FromBody]Activity activity)
        {
            activity.Id = id;
            return HandleResult(await this.Mediator.Send(new Edit.Command{ Activity = activity }));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteActivity([FromRoute]Guid id)
        {
            return HandleResult(await this.Mediator.Send(new Delete.Command{ Id = id }));
        }
    }
}