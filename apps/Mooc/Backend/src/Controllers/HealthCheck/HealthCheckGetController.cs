namespace Mooc.Backend.Controllers.HealthCheck
{
    using Microsoft.AspNetCore.Mvc;
    using src.Shared.Infrastructure;

    [Route("health-check")]
    public class HealthCheckGetController : Controller
    {
        public HealthCheckGetController(RandomNumberGenerator generator)
        {
            Generator = generator;
        }

        private RandomNumberGenerator Generator { get; }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Index()
        {
            return Ok(new {moocBackend = "ok", rand = Generator.Generate()});
        }
    }
}