namespace receiptProject.Services
{
    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> GetAllReceiptsAsync(int userId);
        Task<Receipt?> GetReceiptByIdAsync(int receiptId, int userId);
        Task<Receipt> AddReceiptAsync(Receipt receipt);
        Task<bool> UpdateReceiptAsync(Receipt receipt);
        Task<bool> DeleteReceiptAsync(int receiptId, int userId);
    }
} 