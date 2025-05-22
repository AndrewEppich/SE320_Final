// using receiptProject.Services;
// using Xunit;
// using receiptProject.Services;
// namespace BackendTests;
//
// using BackendTests_AmountBasedReceiptFilterTests = BackendTests.AmountBasedReceiptFilterTests;
//
// public class AmountBasedReceiptFilterTests
// {
//     [Theory]
//     [InlineData(1,5)]
//     [InlineData(2,3)]
//     [InlineData(.50,7)]
//     public void AssertNotNullAmountBasedReceiptFilter(int min, int max)
//     {
//         List<Receipt> testIfEmpty = AmountBasedReceiptFilter.FilterByAmount(min, max);
//         Assert.NotEmpty(testIfEmpty);
//     }
// }