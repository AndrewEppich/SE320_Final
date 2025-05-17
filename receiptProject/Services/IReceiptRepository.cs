using System.Collections.Generic;
using System.Threading.Tasks;

namespace receiptProject.Services
{
    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> GetAllReceiptsAsync(int userId);
        Task<Receipt?> GetReceiptByIdAsync(int receiptId, int userId);
        Task<Receipt> AddReceiptAsync(Receipt receipt);
        Task<bool> UpdateReceiptAsync(Receipt receipt);
        Task<bool> DeleteReceiptAsync(int receiptId, int userId);
        Task<IEnumerable<Receipt>> GetReceiptsByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Receipt>> GetReceiptsByAmountRangeAsync(int userId, decimal minAmount, decimal maxAmount);
        Task<IEnumerable<Receipt>> GetReceiptsByVendorAsync(int userId, string vendor);
    }
} 