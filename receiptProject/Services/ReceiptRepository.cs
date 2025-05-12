using Microsoft.EntityFrameworkCore;
using receiptProject.Data;

namespace receiptProject.Services
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;

        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Receipt>> GetAllReceiptsAsync(int userId)
        {
            return await _context.Receipts
                .Where(r => r.UserID == userId)
                .Include(r => r.Items)
                .ToListAsync();
        }

        public async Task<Receipt?> GetReceiptByIdAsync(int receiptId, int userId)
        {
            return await _context.Receipts
                .Where(r => r.ReceiptID == receiptId && r.UserID == userId)
                .Include(r => r.Items)
                .FirstOrDefaultAsync();
        }

        public async Task<Receipt> AddReceiptAsync(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<bool> UpdateReceiptAsync(Receipt receipt)
        {
            _context.Entry(receipt).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ReceiptExists(receipt.ReceiptID))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteReceiptAsync(int receiptId, int userId)
        {
            var receipt = await _context.Receipts
                .FirstOrDefaultAsync(r => r.ReceiptID == receiptId && r.UserID == userId);
                
            if (receipt == null)
            {
                return false;
            }

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> ReceiptExists(int receiptId)
        {
            return await _context.Receipts.AnyAsync(r => r.ReceiptID == receiptId);
        }
    }
} 