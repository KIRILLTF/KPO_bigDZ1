// BankAccountTests.cs
using Xunit;

namespace FinancialAccounting.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void Deposit_ShouldIncreaseBalance()
        {
            var account = new BankAccount(1, "Test Account", 100);
            account.Deposit(50);
            Assert.Equal(150, account.Balance);
        }

        [Fact]
        public void Withdraw_ShouldDecreaseBalance()
        {
            var account = new BankAccount(1, "Test Account", 100);
            account.Withdraw(50);
            Assert.Equal(50, account.Balance);
        }

        [Fact]
        public void Withdraw_ShouldThrowException_WhenInsufficientFunds()
        {
            var account = new BankAccount(1, "Test Account", 100);
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(150));
        }
    }
}