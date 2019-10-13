namespace MoocApps.Backend.Controllers.Courses
{
    using Microsoft.AspNetCore.Mvc;

    [Route("courses")]
    public class CoursesPutController : Controller
    {
        [HttpPut("{id}")]
        public IActionResult Index(string id, [FromBody] object body)
        {
            return StatusCode(201);
        }
    }
}