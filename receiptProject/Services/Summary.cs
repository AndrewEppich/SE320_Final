using System.Text.Json.Serialization;

namespace receiptProject.Services
{
    public class Summary
    {
        public int SummaryID { get; set; }
        public int UserID { get; set; }
        public string SummaryType { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalSpent { get; set; }
        public string? DataJson { get; set; }


        [JsonIgnore]
        public User? User { get; set; }
    }
} 