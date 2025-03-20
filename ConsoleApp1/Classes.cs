namespace FinancialAccounting
{
    // Класс BankAccount представляет банковский счет
    public class BankAccount
    {
        public int Id { get; set; } // Уникальный идентификатор счета
        public string Name { get; set; } // Название счета
        public decimal Balance { get; set; } // Текущий баланс счета

        public BankAccount(int id, string name, decimal balance)
        {
            Id = id;
            Name = name;
            Balance = balance;
        }

        // Метод для пополнения счета
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }

        // Метод для списания со счета
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }
    }

    // Перечисление для типа категории (доход или расход)
    public enum CategoryType
    {
        Income,
        Expense
    }

    // Класс Category представляет категорию операции
    public class Category
    {
        public int Id { get; set; } // Уникальный идентификатор категории
        public CategoryType Type { get; set; } // Тип категории (доход или расход)
        public string Name { get; set; } // Название категории

        public Category(int id, CategoryType type, string name)
        {
            Id = id;
            Type = type;
            Name = name;
        }
    }

    // Перечисление для типа операции (доход или расход)
    public enum OperationType
    {
        Income,
        Expense
    }

    // Класс Operation представляет финансовую операцию
    public class Operation
    {
        public int Id { get; set; } // Уникальный идентификатор операции
        public OperationType Type { get; set; } // Тип операции (доход или расход)
        public int BankAccountId { get; set; } // Ссылка на счет
        public decimal Amount { get; set; } // Сумма операции
        public DateTime Date { get; set; } // Дата операции
        public string Description { get; set; } // Описание операции (необязательное поле)
        public int CategoryId { get; set; } // Ссылка на категорию

        public Operation(int id, OperationType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string description = null)
        {
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
        }
    }
}