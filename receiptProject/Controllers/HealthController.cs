using Microsoft.AspNetCore.Mvc;
using receiptProject.Data;

namespace receiptProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HealthController> _logger;

        public HealthController(AppDbContext context, ILogger<HealthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                bool isDatabaseConnected = _context.Database.CanConnect();

                if (!isDatabaseConnected)
                {
                    _logger.LogWarning("Database connection check failed");
                    return StatusCode(503, new { status = "error", message = "Database connection failed" });
                }

                return Ok(new
                {
                    status = "healthy",
                    time = DateTime.UtcNow,
                    database = "connected"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Health check failed");
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    }
} 