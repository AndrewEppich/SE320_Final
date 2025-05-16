using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using receiptProject.Data;
using receiptProject.Models;
using receiptProject.Services;
using Xunit;

namespace receiptProject.Tests
{
    public class ReceiptRepositoryTests
    {
        private readonly ReceiptRepository _repository;
        private readonly AppDbContext _context;

        public ReceiptRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new AppDbContext(options);
            _repository = new ReceiptRepository(_context);

            _context.Receipts.AddRange(
                new Receipt { ReceiptID = 1, UserID = 1, Vendor = "Amazon", Amount = 100, PurchaseDate = DateTime.Today },
                new Receipt { ReceiptID = 2, UserID = 1, Vendor = "Target", Amount = 200, PurchaseDate = DateTime.Today.AddDays(-1) }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetReceiptByIdAsync_ReturnsCorrectReceipt()
        {
            var receipt = await _repository.GetReceiptByIdAsync(1);
            Assert.NotNull(receipt);
            Assert.Equal("Amazon", receipt.Vendor);
        }

        [Fact]
        public async Task DeleteReceiptAsync_RemovesReceipt()
        {
            var success = await _repository.DeleteReceiptAsync(1);
            Assert.True(success);
            var deleted = await _repository.GetReceiptByIdAsync(1);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task FilterReceiptsAsync_FiltersByVendor()
        {
            var results = await _repository.FilterReceiptsAsync(null, null, null, null, "Target");
            Assert.Single(results);
        }
    }
}
