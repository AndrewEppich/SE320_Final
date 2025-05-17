using Google.Cloud.Vision.V1;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace receiptProject.Services
{
    public class ReceiptImageProcessor
    {
        private readonly ImageAnnotatorClient _visionClient;
        private readonly ILogger<ReceiptImageProcessor> _logger;

        public ReceiptImageProcessor(ILogger<ReceiptImageProcessor> logger, IConfiguration configuration)
        {
            var credentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "se320final-42ad9e1a7f6d.json");
            if (!File.Exists(credentialsPath))
            {
                throw new InvalidOperationException($"Google Cloud credentials file not found at: {credentialsPath}");
            }

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
            _visionClient = ImageAnnotatorClient.Create();
            _logger = logger;
        }

        public async Task<Receipt> ProcessReceiptImage(Stream imageStream)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await imageStream.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                var image = Image.FromBytes(imageBytes);

                var response = await _visionClient.DetectTextAsync(image);
                var fullText = response.FullTextAnnotation?.Text ?? string.Empty;

                var receipt = new Receipt
                {
                    PurchaseDate = ExtractDate(fullText),
                    Amount = ExtractAmount(fullText),
                    Vendor = ExtractVendor(fullText),
                    MetadataJson = fullText 
                };

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
                var match = Regex.Match(text, pattern);
                if (match.Success)
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
                @"\$?\s*(\d+\.\d{2})\s*TOTAL"
            };

            foreach (var pattern in totalPatterns)
            {
                var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
                if (match.Success && decimal.TryParse(match.Groups[1].Value, out decimal amount))
                {
                    return amount;
                }
            }

            return null;
        }

        private string? ExtractVendor(string text)
        {
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedLine) && 
                    !trimmedLine.Contains("RECEIPT") && 
                    !trimmedLine.Contains("TOTAL") &&
                    !trimmedLine.Contains("DATE") &&
                    !trimmedLine.Contains("TIME"))
                {
                    return trimmedLine;
                }
            }

            return null;
        }
    }
} 