using System;
using System.Collections.Generic;
using System.Linq;

namespace receiptProject.receiptProjectBackend.Services
{
    public class WeeklySummaryFilter
    {

        public List<Summary> GenerateWeeklySummaries(List<Receipt> receipts)
        {
            return receipts
                .GroupBy(r => System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                    r.PurchaseDate, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                .Select(group => new Summary
                {
                    Period = $"Week {group.Key}",
                    TotalAmount = group.Sum(r => r.Amount),
                    ReceiptCount = group.Count()
                })
                .ToList();
        }
    }
}
