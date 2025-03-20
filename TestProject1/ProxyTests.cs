// FinancialProxyTests.cs
using System.Collections.Generic;
using Xunit;

namespace FinancialAccounting.Tests
{
    public class FinancialProxyTests
    {
        [Fact]
        public void GetBankAccounts_ShouldCacheAccounts()
        {
            // Arrange
            var facade = new FinancialFacade();
            var account = new BankAccount(1, "Test Account", 100);
            facade.AddBankAccount(account);

            var proxy = new FinancialProxy(facade);

            // Act
            var result1 = proxy.GetBankAccounts();
            var result2 = proxy.GetBankAccounts();

            // Assert
            Assert.Single(result1);
            Assert.Same(result1, result2); // Проверяем, что возвращается кэшированный список
        }

        [Fact]
        public void GetBankAccounts_ShouldReturnAccountsFromFacade()
        {
            // Arrange
            var facade = new FinancialFacade();
            var account1 = new BankAccount(1, "Test Account 1", 100);
            var account2 = new BankAccount(2, "Test Account 2", 200);
            facade.AddBankAccount(account1);
            facade.AddBankAccount(account2);

            var proxy = new FinancialProxy(facade);

            // Act
            var result = proxy.GetBankAccounts();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(account1, result);
            Assert.Contains(account2, result);
        }
    }
}