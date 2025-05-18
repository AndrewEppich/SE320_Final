using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using receiptProject.Models;

namespace receiptProject.Services
{
    public static class ReceiptFormatParser
    {
        public static List<ReceiptItem> ParseItems(string rawText)
        {
            var items = new List<ReceiptItem>();

            if (rawText.Contains("Walmart"))
            {
                items = ParseWalmartFormat(rawText);
            }
            else if (rawText.Contains("Target"))
            {
                items = ParseTargetFormat(rawText);
            }
            else
            {
                items = ParseGenericFormat(rawText);
            }

            return items;
        }

        private static List<ReceiptItem> ParseWalmartFormat(string rawText)
        {
            var items = new List<ReceiptItem>();
            var regex = new Regex(@"(?<name>.+?)\s+(?<price>\d+\.\d{2})");

            foreach (Match match in regex.Matches(rawText))
            {
                items.Add(new ReceiptItem
                {
                    ItemName = match.Groups["name"].Value.Trim(),
                    Price = decimal.Parse(match.Groups["price"].Value),
                    RawText = match.Value
                });
            }

            return items;
        }

        private static List<ReceiptItem> ParseTargetFormat(string rawText)
        {
            var items = new List<ReceiptItem>();
            var regex = new Regex(@"\*\s(?<name>.+?)\s+(?<price>\d+\.\d{2})");

            foreach (Match match in regex.Matches(rawText))
            {
                items.Add(new ReceiptItem
                {
                    ItemName = match.Groups["name"].Value.Trim(),
                    Price = decimal.Parse(match.Groups["price"].Value),
                    RawText = match.Value
                });
            }

            return items;
        }

        private static List<ReceiptItem> ParseGenericFormat(string rawText)
        {
            var items = new List<ReceiptItem>();
            var regex = new Regex(@"(?<name>.+?)\s+(?<price>\d+\.\d{2})");

            foreach (Match match in regex.Matches(rawText))
            {
                items.Add(new ReceiptItem
                {
                    ItemName = match.Groups["name"].Value.Trim(),
                    Price = decimal.Parse(match.Groups["price"].Value),
                    RawText = match.Value
                });
            }

            return items;
        }
    }
}
