using System.Globalization;

namespace FinancialAccounting
{
    class Program
    {
        static void Main(string[] args)
        {
            FinancialFacade facade = new FinancialFacade();
            FinancialFactory factory = new FinancialFactory();
            Importer importer = new Importer();
            Exporter exporter = new Exporter();
            FinancialProxy proxy = new FinancialProxy(facade);

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Учет финансов ===\n" + 
                    "1. Создать счет\n" +
                    "2. Создать категорию\n" +
                    "3. Создать операцию\n" +
                    "4. Пополнить счет\n" +
                    "5. Списать со счета\n" +
                    "6. Удалить счет\n" +
                    "7. Удалить категорию\n" +
                    "8. Удалить операцию\n" +
                    "9. Показать все счета\n" +
                    "10. Показать все категории\n" +
                    "11. Показать все операции\n" +
                    "12. Импорт операций\n" +
                    "13. Экспорт операций\n" +
                    "14. Завершить программу\n" +
                    "Выберите действие:"
                    );

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateBankAccount(facade, factory);
                        break;
                    case "2":
                        CreateCategory(facade, factory);
                        break;
                    case "3":
                        CreateOperation(facade, factory);
                        break;
                    case "4":
                        DepositToAccount(facade);
                        break;
                    case "5":
                        WithdrawFromAccount(facade);
                        break;
                    case "6":
                        DeleteBankAccount(facade);
                        break;
                    case "7":
                        DeleteCategory(facade);
                        break;
                    case "8":
                        DeleteOperation(facade);
                        break;
                    case "9":
                        ShowAllBankAccounts(proxy);
                        break;
                    case "10":
                        ShowAllCategories(facade);
                        break;
                    case "11":
                        ShowAllOperations(facade);
                        break;
                    case "12":
                        ImportOperations(facade, importer);
                        break;
                    case "13":
                        ExportOperations(facade, exporter);
                        break;
                    case "14":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Программа завершена.");
        }

        private static void CreateBankAccount(FinancialFacade facade, FinancialFactory factory)
        {
            string name = ReadString("Введите название счета: ");
            decimal balance = ReadDecimal("Введите начальный баланс: ");

            var account = factory.CreateBankAccount(facade.GetBankAccounts().Count + 1, name, balance);
            facade.AddBankAccount(account);
            Console.WriteLine($"Счет '{name}' создан.");
        }

        private static void CreateCategory(FinancialFacade facade, FinancialFactory factory)
        {
            Console.Write("Введите тип категории (1 - Доход, 2 - Расход): ");
            int typeInput = ReadIntInRange(1, 2);
            CategoryType type = typeInput == 1 ? CategoryType.Income : CategoryType.Expense;

            string name = ReadString("Введите название категории: ");

            var category = factory.CreateCategory(facade.GetCategories().Count + 1, type, name);
            facade.AddCategory(category);
            Console.WriteLine($"Категория '{name}' создана.");
        }

        private static void CreateOperation(FinancialFacade facade, FinancialFactory factory)
        {
            if (facade.GetBankAccounts().Count == 0 || facade.GetCategories().Count == 0)
            {
                Console.WriteLine("Сначала создайте счет и категорию.");
                return;
            }

            Console.Write("Введите тип операции (1 - Доход, 2 - Расход): ");
            int typeInput = ReadIntInRange(1, 2);
            OperationType type = typeInput == 1 ? OperationType.Income : OperationType.Expense;

            decimal amount = ReadDecimal("Введите сумму: ");
            DateTime date = ReadDate("Введите дату (гггг-мм-дд): ");

            var category = SelectCategory(facade);
            if (category == null)
            {
                Console.WriteLine("Категория не найдена.");
                return;
            }

            string description = ReadString("Введите описание (необязательно): ");

            var account = SelectBankAccount(facade);
            var operation = factory.CreateOperation(facade.GetOperations().Count + 1, type, account.Id, amount, date, category.Id, description);
            facade.AddOperation(operation);
            Console.WriteLine("Операция создана.");
        }

        private static void DepositToAccount(FinancialFacade facade)
        {
            if (facade.GetBankAccounts().Count == 0)
            {
                Console.WriteLine("Сначала создайте счет.");
                return;
            }

            var account = SelectBankAccount(facade);
            decimal amount = ReadDecimal("Введите сумму для пополнения: ");

            account.Deposit(amount);
            Console.WriteLine($"Счет '{account.Name}' пополнен на {amount}. Новый баланс: {account.Balance}");
        }

        private static void WithdrawFromAccount(FinancialFacade facade)
        {
            if (facade.GetBankAccounts().Count == 0)
            {
                Console.WriteLine("Сначала создайте счет.");
                return;
            }

            var account = SelectBankAccount(facade);
            decimal amount = ReadDecimal("Введите сумму для списания: ");

            try
            {
                account.Withdraw(amount);
                Console.WriteLine($"Со счета '{account.Name}' списано {amount}. Новый баланс: {account.Balance}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DeleteBankAccount(FinancialFacade facade)
        {
            if (facade.GetBankAccounts().Count == 0)
            {
                Console.WriteLine("Нет доступных счетов для удаления.");
                return;
            }

            var account = SelectBankAccount(facade);
            if (facade.GetOperations().Any(op => op.BankAccountId == account.Id))
            {
                Console.WriteLine("Невозможно удалить счет, так как он используется в операциях.");
                return;
            }

            facade.GetBankAccounts().Remove(account);
            Console.WriteLine($"Счет '{account.Name}' удален.");
        }

        private static void DeleteCategory(FinancialFacade facade)
        {
            if (facade.GetCategories().Count == 0)
            {
                Console.WriteLine("Нет доступных категорий для удаления.");
                return;
            }

            var category = SelectCategory(facade);
            if (facade.GetOperations().Any(op => op.CategoryId == category.Id))
            {
                Console.WriteLine("Невозможно удалить категорию, так как она используется в операциях.");
                return;
            }

            facade.GetCategories().Remove(category);
            Console.WriteLine($"Категория '{category.Name}' удалена.");
        }

        private static void DeleteOperation(FinancialFacade facade)
        {
            if (facade.GetOperations().Count == 0)
            {
                Console.WriteLine("Нет доступных операций для удаления.");
                return;
            }

            var operation = SelectOperation(facade);
            facade.GetOperations().Remove(operation);
            Console.WriteLine("Операция удалена.");
        }

        private static BankAccount SelectBankAccount(FinancialFacade facade)
        {
            Console.WriteLine("\nСписок счетов:");
            foreach (var account in facade.GetBankAccounts())
            {
                Console.WriteLine($"{account.Id}: {account.Name} - {account.Balance}");
            }

            int accountId;
            while (true)
            {
                accountId = ReadInt("Введите ID счета: ");
                var account = facade.GetBankAccounts().FirstOrDefault(a => a.Id == accountId);
                if (account != null)
                    return account;
                Console.WriteLine($"Счет с ID {accountId} не найден. Попробуйте снова.");
            }
        }

        private static Category SelectCategory(FinancialFacade facade)
        {
            Console.WriteLine("\nСписок категорий:");
            foreach (var category in facade.GetCategories())
            {
                Console.WriteLine($"{category.Id}: {category.Name} ({category.Type})");
            }

            int categoryId;
            while (true)
            {
                categoryId = ReadInt("Введите ID категории: ");
                var category = facade.GetCategories().FirstOrDefault(c => c.Id == categoryId);
                if (category != null)
                    return category;
                Console.WriteLine($"Категория с ID {categoryId} не найдена. Попробуйте снова.");
            }
        }

        private static Operation SelectOperation(FinancialFacade facade)
        {
            Console.WriteLine("\nСписок операций:");
            foreach (var operation in facade.GetOperations())
            {
                Console.WriteLine($"{operation.Id}: {operation.Type} - {operation.Amount} - {operation.Description}");
            }

            int operationId;
            while (true)
            {
                operationId = ReadInt("Введите ID операции: ");
                var operation = facade.GetOperations().FirstOrDefault(op => op.Id == operationId);
                if (operation != null)
                    return operation;
                Console.WriteLine($"Операция с ID {operationId} не найдена. Попробуйте снова.");
            }
        }

        private static void ShowAllBankAccounts(FinancialProxy proxy)
        {
            Console.WriteLine("\nСписок счетов (через прокси):");
            foreach (var account in proxy.GetBankAccounts())
            {
                Console.WriteLine($"{account.Id}: {account.Name} - {account.Balance}");
            }
        }

        private static void ShowAllCategories(FinancialFacade facade)
        {
            Console.WriteLine("\nСписок категорий:");
            foreach (var category in facade.GetCategories())
            {
                Console.WriteLine($"{category.Id}: {category.Name} ({category.Type})");
            }
        }

        private static void ShowAllOperations(FinancialFacade facade)
        {
            Console.WriteLine("\nСписок операций:");
            foreach (var operation in facade.GetOperations())
            {
                Console.WriteLine($"{operation.Id}: {operation.Type} - {operation.Amount} - {operation.Description}");
            }
        }

        private static void ImportOperations(FinancialFacade facade, Importer importer)
        {
            Console.WriteLine("Введите операции в формате: Тип|Сумма|Дата|Категория|Описание");
            Console.WriteLine("Пример: Расход|50|2023-10-01|Кафе|Обед в кафе");
            Console.WriteLine("Введите несколько операций, разделяя их новой строкой:");

            string input = "";
            string line;
            while ((line = Console.ReadLine()) != "")
            {
                input += line + Environment.NewLine;
            }

            try
            {
                var operations = importer.ImportOperations(input, facade.GetCategories(), facade.GetBankAccounts());
                foreach (var operation in operations)
                {
                    facade.AddOperation(operation);
                }

                Console.WriteLine("\nОперации успешно импортированы.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка импорта: {ex.Message}");
            }
        }

        private static void ExportOperations(FinancialFacade facade, Exporter exporter)
        {
            try
            {
                string exportedData = exporter.ExportOperations(facade.GetOperations(), facade.GetCategories());
                Console.WriteLine("\nЭкспортированные операции:");
                Console.WriteLine(exportedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка экспорта: {ex.Message}");
            }
        }

        // Методы для безопасного ввода
        private static int ReadInt(string prompt)
        {
            int result;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                Console.Write(prompt);
            }
            return result;
        }

        private static int ReadIntInRange(int min, int max)
        {
            int result;
            while (true)
            {
                result = ReadInt($"Введите число от {min} до {max}: ");
                if (result >= min && result <= max)
                    break;
                Console.WriteLine($"Число должно быть от {min} до {max}.");
            }
            return result;
        }

        private static decimal ReadDecimal(string prompt)
        {
            decimal result;
            Console.Write(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
                Console.Write(prompt);
            }
            return result;
        }

        private static DateTime ReadDate(string prompt)
        {
            DateTime result;
            Console.Write(prompt);
            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                Console.WriteLine("Некорректный формат даты. Используйте формат гггг-мм-дд.");
                Console.Write(prompt);
            }
            return result;
        }

        private static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}