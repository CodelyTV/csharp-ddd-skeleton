namespace CodelyTv.Apps.Mooc.Backend.Controller.Courses
{
    using System;
    using System.Threading.Tasks;
    using CodelyTv.Mooc.Courses.Application.Create;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Shared.Domain.Bus.Command;

    [Route("courses")]
    public class CoursesPutController : Controller
    {
        private readonly ICommandBus _bus;

        public CoursesPutController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Index(string id, [FromBody] dynamic body)
        {
            body = JsonConvert.DeserializeObject(Convert.ToString(body));

            await this._bus.Dispatch(new CreateCourseCommand(id, body["name"].ToString(), body["duration"].ToString()));

            return StatusCode(201);
        }
    }
}