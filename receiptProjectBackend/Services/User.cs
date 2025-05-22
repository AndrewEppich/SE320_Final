using System.Text.Json.Serialization;

namespace receiptProject.receiptProjectBackend.Services{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        [JsonIgnore]
        public List<Receipt> Receipts { get; set; } = new List<Receipt>();
        
        [JsonIgnore]
        public List<Summary> Summaries { get; set; } = new List<Summary>();
    }
} 