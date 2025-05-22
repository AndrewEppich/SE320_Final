using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using receiptProject.receiptProjectBackend.Data;
using System.Linq;
using System.Text.Json;

namespace receiptProject.receiptProjectBackend.Services {
    public class WeeklySummaryFilter {
        private readonly AppDbContext _context;

        public WeeklySummaryFilter(AppDbContext context) {
            _context = context;
        }

        public async Task<object> GetWeeklySummary(int userId, DateTime startDate) {
            var endDate = startDate.AddDays(7);

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
                    dailyTotals = new List<object>(),
                    receipts = new List<object>()
                };
            }

            decimal totalSpent = receipts.Sum(r => r.Amount ?? 0);

            var dailyTotals = receipts
                .GroupBy(r => r.PurchaseDate?.DayOfWeek)
                .Select(g => new {
                    DayOfWeek = g.Key,
                    Total = g.Sum(r => r.Amount ?? 0),
                    Count = g.Count()
                })
                .ToList();

            return new {
                totalReceipts = receipts.Count,
                totalSpent,
                dailyTotals,
                receipts
            };
        }
    }
} 