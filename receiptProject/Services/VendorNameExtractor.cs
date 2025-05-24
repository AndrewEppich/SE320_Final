using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace receiptProject.Services
{
    public class VendorNameExtractor
    {
        private readonly List<string> knownVendors = new List<string>
        {
            "Walmart", "Target", "Amazon", "Starbucks", "Costco", "Best Buy", "McDonald's", "Shell", "Chevron"
        };

        public string ExtractVendorName(string ocrText)
        {
            // Try exact matches first
            foreach (var vendor in knownVendors)
            {
                if (ocrText.IndexOf(vendor, StringComparison.OrdinalIgnoreCase) >= 0)
                    return vendor;
            }

            // Fallback: look for lines in all caps or title case near the top of receipt
            var lines = ocrText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines.Take(5))
            {
                string cleaned = line.Trim();
                if (Regex.IsMatch(cleaned, @"^[A-Z][A-Za-z\s&,'-.]{2,}$") && cleaned.Length <= 40)
                    return cleaned;
            }

            return "Unknown Vendor";
        }
    }
}