using Google.Cloud.Vision.V1;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace receiptProject.receiptProjectBackend.Services
{
    public class ReceiptImageProcessor
    {
        private readonly ImageAnnotatorClient _visionClient;
        private readonly ILogger<ReceiptImageProcessor> _logger;

        public ReceiptImageProcessor(ILogger<ReceiptImageProcessor> logger, IConfiguration configuration)
        {
            try
            {

                var credentialsJson = Path.Combine(Directory.GetCurrentDirectory(), "receiptProjectBackend",
                    "googleVisionKey.json");
                if (string.IsNullOrEmpty(credentialsJson))
                {
                    throw new InvalidOperationException("Google Cloud credentials not found in configuration");
                }

                var tempPath = Path.GetTempFileName();
                File.WriteAllText(tempPath, credentialsJson);
                
                _logger = logger;
            }
            catch (Exception ex)
            {
               logger.LogError(ex, "Failed to initialize Google Cloud Vision client");
                throw;
            }
        }

        public async Task<Receipt> ProcessReceiptImage(Stream imageStream)
        {
            if (imageStream == null || imageStream.Length == 0)
            {
                throw new ArgumentException("Image stream is empty or null");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await imageStream.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                var image = Image.FromBytes(imageBytes);
                var response = await _visionClient.DetectDocumentTextAsync(image);
                
                if (response == null || string.IsNullOrEmpty(response.Text))
                {
                    throw new InvalidOperationException("No text could be detected in the image");
                }

                var fullText = response.Text;
                _logger.LogInformation("Successfully extracted text from receipt image");

                var receipt = new Receipt
                {
                    PurchaseDate = ExtractDate(fullText),
                    Amount = ExtractAmount(fullText),
                    Vendor = ExtractVendor(fullText),
                    MetadataJson = fullText
                };

                if (receipt.Amount == null && receipt.Vendor == null && receipt.PurchaseDate == null)
                {
                    _logger.LogWarning("Could not extract any receipt information from the text");
                }

                return receipt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing receipt image");
                throw;
            }
        }

        private DateTime? ExtractDate(string text)
        {
            var datePatterns = new[]
            {
                @"\d{1,2}/\d{1,2}/\d{2,4}",
                @"\d{1,2}-\d{1,2}-\d{2,4}",
                @"\d{1,2}\.\d{1,2}\.\d{2,4}"
            };

            foreach (var pattern in datePatterns)
            {
                var matches = Regex.Matches(text, pattern);
                foreach (Match match in matches)
                {
                    if (DateTime.TryParse(match.Value, out DateTime date))
                    {
                        return date;
                    }
                }
            }

            return null;
        }

        private decimal? ExtractAmount(string text)
        {
            var totalPatterns = new[]
            {
                @"TOTAL\s*\$?\s*(\d+\.\d{2})",
                @"TOTAL\s*DUE\s*\$?\s*(\d+\.\d{2})",
                @"AMOUNT\s*DUE\s*\$?\s*(\d+\.\d{2})",
                @"\$?\s*(\d+\.\d{2})\s*TOTAL",
                @"\$?\s*(\d+\.\d{2})\s*$"
            };

            foreach (var pattern in totalPatterns)
            {
                var matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    if (decimal.TryParse(match.Groups[1].Value, out decimal amount))
                    {
                        return amount;
                    }
                }
            }

            return null;
        }

        private string? ExtractVendor(string text)
        {
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var commonWords = new[] { "RECEIPT", "TOTAL", "DATE", "TIME", "CASH", "CARD", "PAYMENT", "THANK", "YOU" };
            
            foreach (var line in lines.Take(5))
            {
                var trimmedLine = line.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedLine) && 
                    !commonWords.Any(word => trimmedLine.ToUpper().Contains(word)))
                {
                    return trimmedLine;
                }
            }

            return null;
        }
    }
} 