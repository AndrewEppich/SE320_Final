namespace receiptProject.receiptProjectBackend.Services;

public interface IReceiptObserver
{
    void OnReceiptProcessed(Receipt receipt);
    void OnReceiptError(string error);
}