using receiptProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace receiptProject.Services
{
    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> GetAllReceiptsAsync(int userId);
        Task<Receipt?> GetReceiptByIdAsync(int id);
        Task<bool> DeleteReceiptAsync(int id);
        Task<IEnumerable<Receipt>> FilterReceiptsAsync(DateTime? startDate, DateTime? endDate, decimal? minAmount, decimal? maxAmount, string? vendor);
    }
}
