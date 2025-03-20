namespace FinancialAccounting
{
    // Интерфейс команды
    public interface ICommand
    {
        void Execute();
    }

    // Команда для создания счета
    public class CreateAccountCommand : ICommand
    {
        private readonly FinancialFacade _facade;
        private readonly BankAccount _account;

        public CreateAccountCommand(FinancialFacade facade, BankAccount account)
        {
            _facade = facade;
            _account = account;
        }

        public void Execute()
        {
            _facade.AddBankAccount(_account);
            Console.WriteLine($"Счет '{_account.Name}' создан.");
        }
    }

    // Команда для создания операции
    public class CreateOperationCommand : ICommand
    {
        private readonly FinancialFacade _facade;
        private readonly Operation _operation;

        public CreateOperationCommand(FinancialFacade facade, Operation operation)
        {
            _facade = facade;
            _operation = operation;
        }

        public void Execute()
        {
            _facade.AddOperation(_operation);
            Console.WriteLine($"Операция на сумму {_operation.Amount} создана.");
        }
    }
}