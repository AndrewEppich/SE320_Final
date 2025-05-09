using Microsoft.AspNetCore.Mvc;

namespace receiptProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "Data from .NET backend" });
        }
    }
}