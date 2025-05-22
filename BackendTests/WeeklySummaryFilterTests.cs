/*

using receiptProject.receiptProjectBackend.Services;
using receiptProject.receiptProjectBackend.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BackendTests;

public class WeeklySummaryFilterTests
{
    readonly AppDbContext _context;

    [Theory]
    [MemberData(nameof(WeeklyDateTestData.ValidDate), MemberType = typeof(WeeklyDateTestData))]
    public async Task AssertNotNullAmountBasedReceiptFilter(int userId, DateTime startDate)
    {
        var weekly = new WeeklySummaryFilter(_context);
        var result = await weekly.GetWeeklySummary(userId, startDate);
        Assert.NotNull(result);
        //Assert.NotEmpty((IEnumerable<object>)result);
    }

    [Theory]
    [MemberData(nameof(WeeklyDateTestData.ValidDate), MemberType = typeof(WeeklyDateTestData))]
    public async Task AssertNullAmountBasedReceiptFilter(int userId, DateTime startDate)
    {
        var weekly = new WeeklySummaryFilter(_context);
        var result = await weekly.GetWeeklySummary(userId, startDate);
        Assert.NotNull(result);
        //Assert.NotEmpty((IEnumerable<object>)result);
    }
}

public class WeeklyDateTestData
    {
        public static IEnumerable<object[]> ValidDate =>
            new List<object[]>
            {
                new object[] { 1, new DateTime(2025, 5, 13) },
            };

        public static IEnumerable<object[]> InvalidDate =>
            new List<object[]>
            {
                new object[] { 1, new DateTime(1900, 1, 1) },

            };
    }
    
*/