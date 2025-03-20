namespace FinancialAccounting
{
    // Фасад для работы с финансами
    public class FinancialFacade
    {
        private List<BankAccount> _bankAccounts = new List<BankAccount>();
        private List<Category> _categories = new List<Category>();
        private List<Operation> _operations = new List<Operation>();

        public void AddBankAccount(BankAccount account)
        {
            _bankAccounts.Add(account);
        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void AddOperation(Operation operation)
        {
            _operations.Add(operation);
        }

        public List<BankAccount> GetBankAccounts() => _bankAccounts;
        public List<Category> GetCategories() => _categories;
        public List<Operation> GetOperations() => _operations;
    }
}