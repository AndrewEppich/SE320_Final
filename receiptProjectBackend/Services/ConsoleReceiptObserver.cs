namespace receiptProject.receiptProjectBackend.Services;

public class ConsoleReceiptObserver : IReceiptObserver
{
    public void OnReceiptProcessed(Receipt receipt)
    {
        Console.WriteLine($"Receipt processed successfully:");
        Console.WriteLine($"- Vendor: {receipt.Vendor}");
        Console.WriteLine($"- Amount: ${receipt.Amount}");
        Console.WriteLine($"- Date: {receipt.PurchaseDate}");
        Console.WriteLine("------------------------");
    }

    public void OnReceiptError(string error)
    {
        Console.WriteLine($"Error processing receipt: {error}");
        Console.WriteLine("------------------------");
    }
}