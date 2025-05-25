using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace receiptProject.receiptProjectBackend.Controllers
{
    /// Handles receipt related api endpoints
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptRepository _repository;

        public ReceiptsController(IReceiptRepository repository)
        {
            _repository = repository;
        }

        /// Retrieves all receipts.
        [HttpGet]
        public ActionResult<List<Receipt>> GetAll()
        {
            var receipts = _repository.GetAllReceipts();
            return Ok(receipts);
        }

        /// Gets a single receipt by ID.
        [HttpGet("{id}")]
        public ActionResult<Receipt> GetById(int id)
        {
            var receipt = _repository.GetReceiptById(id);
            if (receipt == null) return NotFound();
            return Ok(receipt);
        }

        /// Adds a new receipt.
        [HttpPost]
        public IActionResult Add([FromBody] Receipt receipt)
        {
            _repository.AddReceipt(receipt);
            return CreatedAtAction(nameof(GetById), new { id = receipt.ReceiptID }, receipt);
        }

        /// Updates an existing receipt
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Receipt receipt)
        {
            if (id != receipt.ReceiptID) return BadRequest();
            _repository.UpdateReceipt(receipt);
            return NoContent();
        }

        /// Deletes a receipt by ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.DeleteReceipt(id);
            return NoContent();
        }
    }
}
