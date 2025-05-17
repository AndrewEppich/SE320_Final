using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using receiptProject.Controllers;
using receiptProject.Models;
using receiptProject.Services;
using Xunit;

namespace receiptProject.Tests
{
    public class ReceiptsControllerTests
    {
        private readonly Mock<IReceiptRepository> _mockRepo;
        private readonly ReceiptsController _controller;

        public ReceiptsControllerTests()
        {
            _mockRepo = new Mock<IReceiptRepository>();
            var logger = new Mock<ILogger<ReceiptsController>>();
            _controller = new ReceiptsController(_mockRepo.Object, logger.Object);
        }

        [Fact]
        public async Task GetReceiptById_ReturnsOk_WhenFound()
        {
            _mockRepo.Setup(r => r.GetReceiptByIdAsync(1))
                .ReturnsAsync(new Receipt { ReceiptID = 1, Vendor = "Amazon" });

            var result = await _controller.GetReceiptById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var receipt = Assert.IsType<Receipt>(okResult.Value);
            Assert.Equal("Amazon", receipt.Vendor);
        }

        [Fact]
        public async Task DeleteReceipt_ReturnsNoContent_WhenDeleted()
        {
            _mockRepo.Setup(r => r.DeleteReceiptAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteReceipt(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task FilterReceipts_ReturnsFilteredResults()
        {
            _mockRepo.Setup(r => r.FilterReceiptsAsync(null, null, null, null, "Target"))
                .ReturnsAsync(new List<Receipt> { new Receipt { Vendor = "Target" } });

            var result = await _controller.FilterReceipts(null, null, null, null, "Target");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var receipts = Assert.IsAssignableFrom<IEnumerable<Receipt>>(okResult.Value);
        }
    }
}
