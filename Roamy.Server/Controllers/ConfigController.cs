using Microsoft.AspNetCore.Mvc;

namespace Roamy.Server.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult ReadMapboxToken()
        {
            return Ok(_configuration["Mapbox:AccessToken"]);
        }
    }
}