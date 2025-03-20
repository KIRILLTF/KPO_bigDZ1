// OperationTests.cs
using Xunit;

namespace FinancialAccounting.Tests
{
    public class OperationTests
    {
        [Fact]
        public void Operation_ShouldInitializeCorrectly()
        {
            var operation = new Operation(1, OperationType.Expense, 1, 50, DateTime.Now, 1, "Test Operation");
            Assert.Equal(1, operation.Id);
            Assert.Equal(OperationType.Expense, operation.Type);
            Assert.Equal(50, operation.Amount);
            Assert.Equal("Test Operation", operation.Description);
        }
    }
}