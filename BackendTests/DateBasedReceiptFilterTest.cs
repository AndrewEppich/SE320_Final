using receiptProject.receiptProjectBackend.Services;
using Xunit;
namespace BackendTests;
//using BackendTests_AmountBasedReceiptFilterTests = BackendTests.AmountBasedReceiptFilterTests;

public class DateBasedReceiptFilterTests 
{ 
    [Theory]
    [MemberData(nameof(DateTestData.ValidDates), MemberType = typeof(DateTestData))]
    public void AssertNotNullAmountBasedReceiptFilter(DateTime startDate, DateTime endDate)
    {
        var getDateRange = new DateBasedReceiptFilter();
        List<Receipt> testIfEmpty = getDateRange.GetReceiptsByDateRange(startDate, endDate);
        Assert.NotEmpty(testIfEmpty);
    }
     
    [Theory]
    [MemberData(nameof(DateTestData.InvalidDates), MemberType = typeof(DateTestData))]
    public void AssertNullAmountBasedReceiptFilter(DateTime startDate, DateTime endDate)
    {
        var getDateRange = new DateBasedReceiptFilter();
        List<Receipt> testIfEmpty = getDateRange.GetReceiptsByDateRange(startDate, endDate);
        Assert.Empty(testIfEmpty);
    }
}

public class DateTestData
{
    public static IEnumerable<object[]> ValidDates =>
        new List<object[]>
        {
            new object[] { new DateTime(2000, 1, 1), new DateTime(2025, 5, 20) },
            new object[] { new DateTime(1960, 1, 1), new DateTime(2025, 1, 1) }
        };
    
    public static IEnumerable<object[]> InvalidDates =>
        new List<object[]>
        {
            new object[] { new DateTime(1900, 1, 1), new DateTime(1901, 5, 20) },
            new object[] { new DateTime(1980, 1, 1), new DateTime(2000, 1, 1) }
        };
}