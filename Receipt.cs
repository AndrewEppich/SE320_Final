
public class Receipt
{
    public int ReceiptID { get; set; }
    public int UserID { get; set; }
    public string Vendor { get; set; }
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string ImagePath { get; set; }
    public string MetadataJson { get; set; }
}