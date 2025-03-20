namespace FinancialAccounting
{
    // Фабрика для создания объектов
    public class FinancialFactory
    {
        public BankAccount CreateBankAccount(int id, string name, decimal balance)
        {
            return new BankAccount(id, name, balance);
        }

        public Category CreateCategory(int id, CategoryType type, string name)
        {
            return new Category(id, type, name);
        }

        public Operation CreateOperation(int id, OperationType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string description = null)
        {
            return new Operation(id, type, bankAccountId, amount, date, categoryId, description);
        }
    }
}