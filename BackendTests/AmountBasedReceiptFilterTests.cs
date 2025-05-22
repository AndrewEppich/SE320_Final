using receiptProject.receiptProjectBackend.Services;
using Xunit;
namespace BackendTests;
//using BackendTests_AmountBasedReceiptFilterTests = BackendTests.AmountBasedReceiptFilterTests;

public class AmountBasedReceiptFilterTests 
{ 
    [Theory]
    [InlineData(1,100)]
    [InlineData(1,50)]
     public void AssertNotNullAmountBasedReceiptFilter(int min, int max)
     {
         var amountBased = new AmountBasedReceiptFilter();
         List<Receipt> testIfEmpty = amountBased.FilterByAmount(min, max);
         Assert.NotEmpty(testIfEmpty);
     }
     
    [Theory]
    [InlineData(100000,500000)]
    public void AssertNullAmountBasedReceiptFilter(int min, int max)
    {
        var amountBased = new AmountBasedReceiptFilter();
        List<Receipt> testIfEmpty = amountBased.FilterByAmount(min, max);
        Assert.Empty(testIfEmpty);
    }
}