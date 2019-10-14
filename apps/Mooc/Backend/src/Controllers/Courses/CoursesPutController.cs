namespace MoocApps.Backend.Controllers.Courses
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Mooc.Courses.Application.Create;
    using Newtonsoft.Json;

    [Route("courses")]
    public class CoursesPutController : Controller
    {
        public CourseCreator Creator { get; private set; }

        public CoursesPutController(CourseCreator creator)
        {
            Creator = creator;
        }

        [HttpPut("{id}")]
        public IActionResult Index(string id, [FromBody] dynamic body)
        {
            body = JsonConvert.DeserializeObject(Convert.ToString(body));

            this.Creator.Invoke(new CreateCourseRequest(id, body["name"].ToString(), body["duration"].ToString()));

            return StatusCode(201);
        }
    }
}