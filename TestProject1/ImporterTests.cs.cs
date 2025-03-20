// ImporterTests.cs
using Xunit;

namespace FinancialAccounting.Tests
{
    public class ImporterTests
    {
        [Fact]
        public void ImportOperations_ShouldParseValidInput()
        {
            var importer = new Importer();
            var input = "Расход|50|2023-10-01|Кафе|Обед в кафе\nДоход|1000|2023-10-05|Зарплата|Зарплата за октябрь";
            var categories = new List<Category>
            {
                new Category(1, CategoryType.Expense, "Кафе"),
                new Category(2, CategoryType.Income, "Зарплата")
            };
            var accounts = new List<BankAccount>
            {
                new BankAccount(1, "Основной счет", 1000)
            };

            var operations = importer.ImportOperations(input, categories, accounts);

            Assert.Equal(2, operations.Count);
            Assert.Equal("Обед в кафе", operations[0].Description);
            Assert.Equal("Зарплата за октябрь", operations[1].Description);
        }

        [Fact]
        public void ImportOperations_ShouldThrowException_WhenInvalidInput()
        {
            var importer = new Importer();
            var input = "Invalid Input";
            var categories = new List<Category>();
            var accounts = new List<BankAccount>();

            Assert.Throws<FormatException>(() => importer.ImportOperations(input, categories, accounts));
        }
    }
}