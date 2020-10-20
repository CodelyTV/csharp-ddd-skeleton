using CodelyTv.Shared.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodelyTv.Apps.Mooc.Backend.Controller.HealthCheck
{
    [Route("health-check")]
    public class HealthCheckGetController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly RandomNumberGenerator _generator;

        public HealthCheckGetController(RandomNumberGenerator generator)
        {
            _generator = generator;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Index()
        {
            return Ok(new {moocBackend = "ok", rand = _generator.Generate()});
        }
    }
}
