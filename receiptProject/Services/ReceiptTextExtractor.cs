using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace receiptProject.Services
{
    public class ReceiptTextExtractor
    {
        public (DateTime? PurchaseDate, decimal? Amount) ExtractDateAndAmount(string ocrText)
        {
            DateTime? extractedDate = ExtractDate(ocrText);
            decimal? extractedAmount = ExtractAmount(ocrText);
            return (extractedDate, extractedAmount);
        }

        private DateTime? ExtractDate(string text)
        {
            string[] datePatterns = {
                @"\b\d{1,2}/\d{1,2}/\d{2,4}\b",        // e.g., 12/31/2023
                @"\b\d{4}-\d{1,2}-\d{1,2}\b",          // e.g., 2023-12-31
                @"\b\d{1,2} [A-Za-z]{3,9} \d{4}\b"     // e.g., 31 December 2023
            };

            foreach (var pattern in datePatterns)
            {
                var match = Regex.Match(text, pattern);
                if (match.Success)
                {
                    if (DateTime.TryParse(match.Value, out DateTime date))
                        return date;
                }
            }

            return null;
        }

        private decimal? ExtractAmount(string text)
        {
            // Look for patterns like $12.34 or 12.34
            var matches = Regex.Matches(text, @"\$?\d+[.,]\d{2}");

            List<decimal> amounts = new List<decimal>();

            foreach (Match match in matches)
            {
                string clean = match.Value.Replace("$", "").Replace(",", ".");
                if (decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
                {
                    amounts.Add(value);
                }
            }

            // Return the largest value, assuming it’s the total
            return amounts.Count > 0 ? amounts.Max() : null;
        }
    }
}