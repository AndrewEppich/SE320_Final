using receiptProject.Models;
using System.Globalization;
using Newtonsoft.Json;

namespace receiptProject.Services
{
    public class Summary
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string SummaryType { get; set; } // e.g., "basic", "monthly-vendor"
        public DateTime GeneratedAt { get; set; }
        public string DataJson { get; set; } // aggregated data as JSON string

        public Summary() { }

        public Summary(User user, List<Receipt> receipts, string summaryType = "basic")
        {
            User = user;
            SummaryType = summaryType;
            GeneratedAt = DateTime.UtcNow;

            // Decide how to generate summary based on type
            DataJson = summaryType switch
            {
                "monthly-vendor" => GenerateMonthlyVendorSummary(receipts),
                _ => GenerateBasicSummary(receipts)
            };
        }

        // Example existing logic for a basic summary (just count + total)
        private string GenerateBasicSummary(List<Receipt> receipts)
        {
            var total = receipts.Sum(r => r.Amount);
            var count = receipts.Count;

            var result = new
            {
                TotalSpent = total,
                ReceiptCount = count
            };

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        // NEW logic for monthly + vendor aggregation
        private string GenerateMonthlyVendorSummary(List<Receipt> receipts)
        {
            var result = receipts
                .Where(r => r.PurchaseDate != null && r.Vendor != null)
                .GroupBy(r => new
                {
                    Month = r.PurchaseDate.ToString("yyyy-MM", CultureInfo.InvariantCulture),
                    r.Vendor
                })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Vendor = g.Key.Vendor,
                    Total = g.Sum(r => r.Amount)
                })
                .OrderBy(g => g.Month)
                .ThenBy(g => g.Vendor)
                .ToList();

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}
