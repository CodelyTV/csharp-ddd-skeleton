namespace CodelyTv.Apps.Mooc.Backend.Controllers.Courses
{
    using System;
    using CodelyTv.Mooc.Courses.Application.Create;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("courses")]
    public class CoursesPutController : Controller
    {
        private CourseCreator Creator { get; set; }

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