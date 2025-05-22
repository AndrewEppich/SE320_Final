using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using receiptProject.receiptProjectBackend.Services;

namespace receiptProject.receiptProjectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryController : ControllerBase
    {
        private readonly WeeklySummaryFilter _weeklySummaryFilter;
        private readonly MonthlySummaryFilter _monthlySummaryFilter;
        private readonly VendorFilter _vendorFilter;
        private readonly ILogger<SummaryController> _logger;

        public SummaryController(
            WeeklySummaryFilter weeklySummaryFilter,
            MonthlySummaryFilter monthlySummaryFilter,
            VendorFilter vendorFilter,
            ILogger<SummaryController> logger)
        {
            _weeklySummaryFilter = weeklySummaryFilter;
            _monthlySummaryFilter = monthlySummaryFilter;
            _vendorFilter = vendorFilter;
            _logger = logger;
        }

        [HttpGet("weekly")]
        public async Task<ActionResult<Summary>> GetWeeklySummary([FromQuery] int year, [FromQuery] int month, [FromQuery] int week)
        {
            try
            {
                int userId = 1;

                var startDate = new DateTime(year, month, 1).AddDays((week - 1) * 7);
                
                var summary = await _weeklySummaryFilter.GetWeeklySummary(userId, startDate);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting weekly summary");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("monthly")]
        public async Task<ActionResult<Summary>> GetMonthlySummary([FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                int userId = 1;
                
                var summary = await _monthlySummaryFilter.GetMonthlySummary(userId, year, month);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting monthly summary");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("vendor")]
        public async Task<ActionResult<Summary>> GetVendorSummary([FromQuery] string name)
        {
            try
            {
                int userId = 1;
                
                var summary = await _vendorFilter.GetVendorSummary(userId, name);
                
                if (summary == null)
                {
                    return NotFound($"No receipts found for vendor '{name}'");
                }
                
                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting vendor summary");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("vendor/receipts")]
        public async Task<ActionResult<List<Receipt>>> GetVendorReceipts([FromQuery] string name, [FromQuery] bool exactMatch = false)
        {
            try
            {
                int userId = 1;
                
                var receipts = await _vendorFilter.GetReceiptsByVendor(userId, name, exactMatch);
                
                if (receipts.Count == 0)
                {
                    return NotFound($"No receipts found for vendor '{name}'");
                }
                
                return Ok(receipts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting vendor receipts");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("vendors")]
        public async Task<ActionResult<List<string>>> GetAllVendors()
        {
            try
            {
                int userId = 1;
                
                var vendors = await _vendorFilter.GetAllVendors(userId);
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting vendors");
                return StatusCode(500, "Internal server error");
            }
        }
    }
} 