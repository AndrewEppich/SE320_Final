using Microsoft.AspNetCore.Mvc;
using receiptProject.receiptProjectBackend.Services;
using System.Linq;

namespace receiptProject.receiptProjectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptRepository _repository;
        private readonly ILogger<ReceiptsController> _logger;
        private readonly ReceiptImageProcessor _imageProcessor;

        public ReceiptsController(
            IReceiptRepository repository, 
            ILogger<ReceiptsController> logger,
            ReceiptImageProcessor imageProcessor)
        {
            _repository = repository;
            _logger = logger;
            _imageProcessor = imageProcessor;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipts([FromQuery] string? sortBy = null, [FromQuery] string? sortOrder = "asc")
        {
            try
            {
                int userId = 1;
                var receipts = await _repository.GetAllReceiptsAsync(userId);

                if (!string.IsNullOrEmpty(sortBy))
                {
                    receipts = sortBy.ToLower() switch
                    {
                        "amount" => sortOrder.ToLower() == "desc" 
                            ? receipts.OrderByDescending(r => r.Amount)
                            : receipts.OrderBy(r => r.Amount),
                        "date" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.PurchaseDate)
                            : receipts.OrderBy(r => r.PurchaseDate),
                        "vendor" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.Vendor)
                            : receipts.OrderBy(r => r.Vendor),
                        _ => receipts
                    };
                }

                return Ok(receipts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting receipts");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceiptsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortOrder = "asc")
        {
            try
            {
                int userId = 1;
                var receipts = await _repository.GetReceiptsByDateRangeAsync(userId, startDate, endDate);

                if (!string.IsNullOrEmpty(sortBy))
                {
                    receipts = sortBy.ToLower() switch
                    {
                        "amount" => sortOrder.ToLower() == "desc" 
                            ? receipts.OrderByDescending(r => r.Amount)
                            : receipts.OrderBy(r => r.Amount),
                        "date" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.PurchaseDate)
                            : receipts.OrderBy(r => r.PurchaseDate),
                        "vendor" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.Vendor)
                            : receipts.OrderBy(r => r.Vendor),
                        _ => receipts
                    };
                }

                return Ok(receipts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting receipts by date range");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("amount-range")]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceiptsByAmountRange(
            [FromQuery] decimal minAmount,
            [FromQuery] decimal maxAmount,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortOrder = "asc")
        {
            try
            {
                int userId = 1;
                var receipts = await _repository.GetReceiptsByAmountRangeAsync(userId, minAmount, maxAmount);

                if (!string.IsNullOrEmpty(sortBy))
                {
                    receipts = sortBy.ToLower() switch
                    {
                        "amount" => sortOrder.ToLower() == "desc" 
                            ? receipts.OrderByDescending(r => r.Amount)
                            : receipts.OrderBy(r => r.Amount),
                        "date" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.PurchaseDate)
                            : receipts.OrderBy(r => r.PurchaseDate),
                        "vendor" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.Vendor)
                            : receipts.OrderBy(r => r.Vendor),
                        _ => receipts
                    };
                }

                return Ok(receipts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting receipts by amount range");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("vendor")]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceiptsByVendor(
            [FromQuery] string vendor,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortOrder = "asc")
        {
            try
            {
                int userId = 1;
                var receipts = await _repository.GetReceiptsByVendorAsync(userId, vendor);

                if (!string.IsNullOrEmpty(sortBy))
                {
                    receipts = sortBy.ToLower() switch
                    {
                        "amount" => sortOrder.ToLower() == "desc" 
                            ? receipts.OrderByDescending(r => r.Amount)
                            : receipts.OrderBy(r => r.Amount),
                        "date" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.PurchaseDate)
                            : receipts.OrderBy(r => r.PurchaseDate),
                        "vendor" => sortOrder.ToLower() == "desc"
                            ? receipts.OrderByDescending(r => r.Vendor)
                            : receipts.OrderBy(r => r.Vendor),
                        _ => receipts
                    };
                }

                return Ok(receipts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting receipts by vendor");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(int id)
        {
            try
            {

                int userId = 1;
                var receipt = await _repository.GetReceiptByIdAsync(id, userId);

                if (receipt == null)
                {
                    return NotFound();
                }

                return Ok(receipt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting receipt {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Receipt>> PostReceipt(Receipt receipt)
        {
            try
            {

                receipt.UserID = 1;
                
                var createdReceipt = await _repository.AddReceiptAsync(receipt);
                return CreatedAtAction(nameof(GetReceipt), new { id = createdReceipt.ReceiptID }, createdReceipt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating receipt");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceipt(int id, Receipt receipt)
        {
            if (id != receipt.ReceiptID)
            {
                return BadRequest();
            }

            try
            {

                receipt.UserID = 1;
                
                bool success = await _repository.UpdateReceiptAsync(receipt);
                if (!success)
                {
                    return NotFound();
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating receipt {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            try
            {

                int userId = 1;
                
                bool success = await _repository.DeleteReceiptAsync(id, userId);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting receipt {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("scan")]
        public async Task<ActionResult<Receipt>> ScanReceipt(IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    return BadRequest("No image file provided");
                }

                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedTypes.Contains(image.ContentType.ToLower()))
                {
                    return BadRequest("Invalid file type. Only JPEG, PNG, and GIF images are allowed.");
                }

                if (image.Length > 10 * 1024 * 1024)
                {
                    return BadRequest("File size too large. Maximum size is 10MB.");
                }

                using var stream = image.OpenReadStream();
                var receipt = await _imageProcessor.ProcessReceiptImage(stream);

                receipt.UserID = 1;

                var createdReceipt = await _repository.AddReceiptAsync(receipt);
                return CreatedAtAction(nameof(GetReceipt), new { id = createdReceipt.ReceiptID }, createdReceipt);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid image data provided");
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Failed to process receipt image");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error scanning receipt image");
                return StatusCode(500, "Error processing receipt image");
            }
        }
    }
} 