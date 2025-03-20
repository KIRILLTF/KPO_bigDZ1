// CategoryTests.cs
using Xunit;

namespace FinancialAccounting.Tests
{
    public class CategoryTests
    {
        [Fact]
        public void Category_ShouldInitializeCorrectly()
        {
            var category = new Category(1, CategoryType.Expense, "Test Category");
            Assert.Equal(1, category.Id);
            Assert.Equal(CategoryType.Expense, category.Type);
            Assert.Equal("Test Category", category.Name);
        }
    }
}