namespace EnterpriseBankingSystem
{
    public class SavingsAccount : BankAccount
    {
        private readonly decimal _interestRate;

        public SavingsAccount(string accountNumber, string owner, decimal initialBalance, decimal interestRate, ILogger logger)
            : base(accountNumber, owner, initialBalance, logger)
        {
            if (interestRate < 0)
                throw new ArgumentOutOfRangeException(nameof(interestRate), "Faiz oranı negatif olamaz.");
            _interestRate = interestRate;
        }

        public override void CalculateInterest()
        {
            decimal interest = Balance * _interestRate;
            if (interest <= 0)
            {
                _logger.Log($"{Owner} hesabı için faiz getirisi oluşmadı.");
                return;
            }

            Deposit(interest);
            _logger.Log($"{Owner} hesabı faiz kazandı.");
        }
    }
}
