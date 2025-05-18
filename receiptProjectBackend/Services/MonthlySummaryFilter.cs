using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using receiptProject.Data;
using System.Linq;
using System.Text.Json;

namespace receiptProject.Services
{
    public class MonthlySummaryFilter
    {
        private readonly AppDbContext _context;

        public MonthlySummaryFilter(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Summary> GetMonthlySummary(int userId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var existingSummary = await _context.Summaries
                .FirstOrDefaultAsync(s => s.UserID == userId && 
                                         s.SummaryType == "monthly" && 
                                         s.StartDate == startDate && 
                                         s.EndDate == endDate);

            if (existingSummary != null)
            {
                return existingSummary;
            }

            var receipts = await _context.Receipts
                .Where(r => r.UserID == userId && 
                           r.PurchaseDate >= startDate && 
                           r.PurchaseDate < endDate)
                .Include(r => r.Items)
                .ToListAsync();

            if (!receipts.Any())
            {
                return new Summary
                {
                    UserID = userId,
                    SummaryType = "monthly",
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalSpent = 0,
                    DataJson = JsonSerializer.Serialize(new { receipts = new List<Receipt>() })
                };
            }

            decimal totalSpent = receipts.Sum(r => r.Amount ?? 0);

            var weeklyTotals = receipts
                .GroupBy(r => (r.PurchaseDate?.Day - 1) / 7 + 1)
                .Select(g => new
                {
                    WeekOfMonth = g.Key,
                    Total = g.Sum(r => r.Amount ?? 0),
                    Count = g.Count()
                })
                .ToList();

            var vendorTotals = receipts
                .GroupBy(r => r.Vendor)
                .Select(g => new
                {
                    Vendor = g.Key,
                    Total = g.Sum(r => r.Amount ?? 0),
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Total)
                .ToList();

            var summaryData = new
            {
                totalReceipts = receipts.Count,
                totalSpent,
                weeklyTotals,
                vendorTotals,
                receipts
            };

            var summary = new Summary
            {
                UserID = userId,
                SummaryType = "monthly",
                StartDate = startDate,
                EndDate = endDate,
                TotalSpent = totalSpent,
                DataJson = JsonSerializer.Serialize(summaryData)
            };

            _context.Summaries.Add(summary);
            await _context.SaveChangesAsync();

            return summary;
        }
    }
} 