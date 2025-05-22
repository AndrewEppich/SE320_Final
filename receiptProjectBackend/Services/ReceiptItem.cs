using System.Text.Json.Serialization;

namespace receiptProject.receiptProjectBackend.Services{
    public class ReceiptItem
    {
        public int ItemID { get; set; }
        public int ReceiptID { get; set; }
        public string? ItemName { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? RawText { get; set; }


        [JsonIgnore]
        public Receipt? Receipt { get; set; }
    }
} 