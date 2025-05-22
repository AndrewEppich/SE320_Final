using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using receiptProject.receiptProjectBackend.Data;
using System.Linq;

namespace receiptProject.receiptProjectBackend.Services{
    public class VendorFilter
    {
        private readonly AppDbContext _context;

        public VendorFilter(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Receipt>> GetReceiptsByVendor(int userId, string vendorName, bool exactMatch = false)
        {
            IQueryable<Receipt> query = _context.Receipts
                .Where(r => r.UserID == userId);

            if (exactMatch)
            {
                query = query.Where(r => r.Vendor == vendorName);
            }
            else
            {
                query = query.Where(r => r.Vendor != null && r.Vendor.Contains(vendorName));
            }

            return await query
                .Include(r => r.Items)
                .OrderByDescending(r => r.PurchaseDate)
                .ToListAsync();
        }

        public async Task<object> GetVendorSummary(int userId, string vendorName)
        {
            var receipts = await GetReceiptsByVendor(userId, vendorName, true);

            if (!receipts.Any())
            {
                return null;
            }

            var startDate = receipts.Min(r => r.PurchaseDate);
            var endDate = receipts.Max(r => r.PurchaseDate);

            decimal totalSpent = receipts.Sum(r => r.Amount ?? 0);

            var monthlyTotals = receipts
                .GroupBy(r => new { r.PurchaseDate?.Year, r.PurchaseDate?.Month })
                .Select(g => new {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(r => r.Amount ?? 0),
                    Count = g.Count()
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            return new {
                vendor = vendorName,
                totalReceipts = receipts.Count,
                totalSpent,
                monthlyTotals,
                receipts
            };
        }

        public async Task<List<string>> GetAllVendors(int userId)
        {
            return await _context.Receipts
                .Where(r => r.UserID == userId && r.Vendor != null)
                .Select(r => r.Vendor)
                .Distinct()
                .OrderBy(v => v)
                .ToListAsync();
        }
    }
} 