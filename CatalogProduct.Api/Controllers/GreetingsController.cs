using CatalogProduct.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreetingsController : ControllerBase
    {
        [HttpGet("{name}")]
        public ActionResult<string> Get([FromServices] IGreeting greeting, string name)
        {
            return greeting.Hello(name);
        }
    }
}