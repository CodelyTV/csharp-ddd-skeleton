using System;
using System.Threading.Tasks;
using CodelyTv.Mooc.Courses.Application.Create;
using CodelyTv.Shared.Domain.Bus.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodelyTv.Apps.Mooc.Backend.Controller.Courses
{
    [Route("courses")]
    public class CoursesPutController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CommandBus _bus;

        public CoursesPutController(CommandBus bus)
        {
            _bus = bus;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Index(string id, [FromBody] dynamic body)
        {
            body = JsonConvert.DeserializeObject(Convert.ToString(body));

            await _bus.Dispatch(new CreateCourseCommand(id, body["name"].ToString(), body["duration"].ToString()));

            return StatusCode(201);
        }
    }
}
