using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using receiptProject.receiptProjectBackend.Data;
using System.Linq;
using System.Text.Json;

namespace receiptProject.receiptProjectBackend.Services{
    public class WeeklySummaryFilter
    {
        private readonly AppDbContext _context;

        public WeeklySummaryFilter(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Summary> GetWeeklySummary(int userId, DateTime startDate)
        {
            var endDate = startDate.AddDays(7);

            var existingSummary = await _context.Summaries
                .FirstOrDefaultAsync(s => s.UserID == userId && 
                                          s.SummaryType == "weekly" && 
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
                    SummaryType = "weekly",
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalSpent = 0,
                    DataJson = JsonSerializer.Serialize(new { receipts = new List<Receipt>() })
                };
            }

            decimal totalSpent = receipts.Sum(r => r.Amount ?? 0);

            var dailyTotals = receipts
                .GroupBy(r => r.PurchaseDate?.DayOfWeek)
                .Select(g => new
                {
                    DayOfWeek = g.Key,
                    Total = g.Sum(r => r.Amount ?? 0),
                    Count = g.Count()
                })
                .ToList();

            var summaryData = new
            {
                totalReceipts = receipts.Count,
                totalSpent,
                dailyTotals,
                receipts
            };

            var summary = new Summary
            {
                UserID = userId,
                SummaryType = "weekly",
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