using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using receiptProject.receiptProjectBackend.Data;
using System.Linq;
using System.Text.Json;

namespace receiptProject.receiptProjectBackend.Services {
    public class MonthlySummaryFilter {
        private readonly AppDbContext _context;

        public MonthlySummaryFilter(AppDbContext context) {
            _context = context;
        }

        public async Task<object> GetMonthlySummary(int userId, int year, int month) {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var receipts = await _context.Receipts
                .Where(r => r.UserID == userId && 
                           r.PurchaseDate >= startDate && 
                           r.PurchaseDate < endDate)
                .Include(r => r.Items)
                .ToListAsync();

            if (!receipts.Any()) {
                return new {
                    totalReceipts = 0,
                    totalSpent = 0,
                    weeklyTotals = new List<object>(),
                    vendorTotals = new List<object>(),
                    receipts = new List<object>()
                };
            }

            decimal totalSpent = receipts.Sum(r => r.Amount ?? 0);

            var weeklyTotals = receipts
                .GroupBy(r => (r.PurchaseDate?.Day - 1) / 7 + 1)
                .Select(g => new {
                    WeekOfMonth = g.Key,
                    Total = g.Sum(r => r.Amount ?? 0),
                    Count = g.Count()
                })
                .ToList();

            var vendorTotals = receipts
                .GroupBy(r => r.Vendor)
                .Select(g => new {
                    Vendor = g.Key,
                    Total = g.Sum(r => r.Amount ?? 0),
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Total)
                .ToList();

            return new {
                totalReceipts = receipts.Count,
                totalSpent,
                weeklyTotals,
                vendorTotals,
                receipts
            };
        }
    }
} 