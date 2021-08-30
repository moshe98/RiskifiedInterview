using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace paymentGatewaySimulation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthcheckController : ControllerBase
    {
        public const string MockServiceUrl = "https://interview.riskxint.com";

        private readonly ILogger<HealthcheckController> _logger;

        public HealthcheckController(ILogger<HealthcheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using var client = new HttpClient();
            var response = client.GetAsync(MockServiceUrl + "/healthcheck").Result;
            return Ok("Mock server responded to health check with response: " + response.ReasonPhrase);
        }
    }
}
