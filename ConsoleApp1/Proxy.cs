namespace FinancialAccounting
{
    // Прокси для кэширования данных
    public class FinancialProxy
    {
        private FinancialFacade _facade;
        private List<BankAccount> _cachedAccounts;

        public FinancialProxy(FinancialFacade facade)
        {
            _facade = facade;
            _cachedAccounts = facade.GetBankAccounts();
        }

        public List<BankAccount> GetBankAccounts()
        {
            if (_cachedAccounts == null)
            {
                _cachedAccounts = _facade.GetBankAccounts();
            }
            return _cachedAccounts;
        }
    }
}