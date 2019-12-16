namespace CodelyTv.Apps.Mooc.Backend.Controllers.Courses
{
    using System;
    using System.Threading.Tasks;
    using CodelyTv.Mooc.Courses.Application.Create;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Route("courses")]
    public class CoursesPutController : Controller
    {
        private readonly CourseCreator _creator;

        public CoursesPutController(CourseCreator creator)
        {
            _creator = creator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Invoke(string id, [FromBody] dynamic body)
        {
            body = JsonConvert.DeserializeObject(Convert.ToString(body));

            await this._creator.Invoke(new CreateCourseRequest(id, body["name"].ToString(), body["duration"].ToString()));

            return StatusCode(201);
        }
    }
}