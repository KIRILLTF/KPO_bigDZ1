// ExporterTests.cs
using Xunit;

namespace FinancialAccounting.Tests
{
    public class ExporterTests
    {
        [Fact]
        public void ExportOperations_ShouldFormatOperationsCorrectly()
        {
            var exporter = new Exporter();
            var operations = new List<Operation>
            {
                new Operation(1, OperationType.Expense, 1, 50, DateTime.Parse("2023-10-01"), 1, "Обед в кафе"),
                new Operation(2, OperationType.Income, 1, 1000, DateTime.Parse("2023-10-05"), 2, "Зарплата за октябрь")
            };
            var categories = new List<Category>
            {
                new Category(1, CategoryType.Expense, "Кафе"),
                new Category(2, CategoryType.Income, "Зарплата")
            };

            var result = exporter.ExportOperations(operations, categories);

            Assert.Contains("Расход|50|2023-10-01|Кафе|Обед в кафе", result);
            Assert.Contains("Доход|1000|2023-10-05|Зарплата|Зарплата за октябрь", result);
        }
    }
}