using Microsoft.AspNetCore.Mvc;

namespace Radicitus.Health.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error() => Problem();
    }
}
