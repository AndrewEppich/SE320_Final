using Microsoft.EntityFrameworkCore;
using receiptProject.Data;
using receiptProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace receiptProject.Services
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;

        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Receipt>> GetAllReceiptsAsync(int userId)
        {
            return await _context.Receipts.Where(r => r.UserID == userId).ToListAsync();
        }

        public async Task<Receipt?> GetReceiptByIdAsync(int id)
        {
            return await _context.Receipts.FirstOrDefaultAsync(r => r.ReceiptID == id);
        }

        public async Task<bool> DeleteReceiptAsync(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return false;
            }

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Receipt>> FilterReceiptsAsync(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount, string? vendor)
        {
            var query = _context.Receipts.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(r => r.PurchaseDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(r => r.PurchaseDate <= endDate.Value);

            if (minAmount.HasValue)
                query = query.Where(r => r.Amount >= minAmount.Value);

            if (maxAmount.HasValue)
                query = query.Where(r => r.Amount <= maxAmount.Value);

            if (!string.IsNullOrEmpty(vendor))
                query = query.Where(r => r.Vendor.ToLower().Contains(vendor.ToLower()));

            return await query.ToListAsync();
        }
    }
}
