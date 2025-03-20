namespace FinancialAccounting
{
    // Класс для импорта данных из строки
    public class Importer
    {
        // Импорт операций из строки (формат: "тип|сумма|дата|категория|описание")
        public List<Operation> ImportOperations(string input, List<Category> categories, List<BankAccount> accounts)
        {
            var operations = new List<Operation>();
            var lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length < 5)
                    throw new FormatException("Некорректный формат строки.");

                var type = parts[0].Trim().Equals("Доход", StringComparison.OrdinalIgnoreCase) ? OperationType.Income : OperationType.Expense;
                var amount = decimal.Parse(parts[1].Trim());
                var date = DateTime.Parse(parts[2].Trim());
                var categoryName = parts[3].Trim();
                var description = parts[4].Trim();

                var category = categories.FirstOrDefault(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
                if (category == null)
                    throw new ArgumentException($"Категория '{categoryName}' не найдена.");

                var account = accounts.FirstOrDefault();
                if (account == null)
                    throw new ArgumentException("Нет доступных счетов.");

                var operation = new Operation(operations.Count + 1, type, account.Id, amount, date, category.Id, description);
                operations.Add(operation);
            }

            return operations;
        }
    }
}