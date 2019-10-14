namespace MoocApps.Backend.Controllers.Courses
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Mooc.Courses.Application;

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
            body = Newtonsoft.Json.JsonConvert.DeserializeObject(Convert.ToString(body));

            string name = body["name"];
            string duration = body["duration"];

            this.Creator.Invoke(id, name, duration);

            return StatusCode(201);
        }
    }
}