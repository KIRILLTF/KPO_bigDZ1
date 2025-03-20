// FinancialFacadeTests.cs
using Xunit;

namespace FinancialAccounting.Tests
{
    public class FinancialFacadeTests
    {
        [Fact]
        public void AddBankAccount_ShouldAddAccount()
        {
            var facade = new FinancialFacade();
            var account = new BankAccount(1, "Test Account", 100);

            facade.AddBankAccount(account);

            Assert.Single(facade.GetBankAccounts());
            Assert.Equal(account, facade.GetBankAccounts().First());
        }

        [Fact]
        public void AddCategory_ShouldAddCategory()
        {
            var facade = new FinancialFacade();
            var category = new Category(1, CategoryType.Expense, "Test Category");

            facade.AddCategory(category);

            Assert.Single(facade.GetCategories());
            Assert.Equal(category, facade.GetCategories().First());
        }

        [Fact]
        public void AddOperation_ShouldAddOperation()
        {
            var facade = new FinancialFacade();
            var account = new BankAccount(1, "Test Account", 100);
            var category = new Category(1, CategoryType.Expense, "Test Category");
            var operation = new Operation(1, OperationType.Expense, account.Id, 50, DateTime.Now, category.Id, "Test Operation");

            facade.AddBankAccount(account);
            facade.AddCategory(category);
            facade.AddOperation(operation);

            Assert.Single(facade.GetOperations());
            Assert.Equal(operation, facade.GetOperations().First());
        }
    }
}