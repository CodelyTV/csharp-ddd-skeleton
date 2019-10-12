namespace Mooc.Backend.Controllers.HealthCheck
{
    using Microsoft.AspNetCore.Mvc;
    using src.Shared.Domain;

    [Route("health-check")]
    public class HealthCheckGetController : Controller
    {
        public HealthCheckGetController(IRandomNumberGenerator generator)
        {
            Generator = generator;
        }

        private IRandomNumberGenerator Generator { get; }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Index()
        {
            return Ok(new {moocBackend = "ok", rand = Generator.Generate()});
        }
    }
}