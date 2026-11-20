namespace EnterpriseBankingSystem
{
    public class CheckingAccount : BankAccount
    {
        private readonly decimal _overdraftLimit;

        public CheckingAccount(string accountNumber, string owner, decimal initialBalance, decimal overdraftLimit, ILogger logger)
            : base(accountNumber, owner, initialBalance, logger)
        {
            if (overdraftLimit < 0)
                throw new ArgumentOutOfRangeException(nameof(overdraftLimit), "Kredi limiti negatif olamaz.");
            _overdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            EnsurePositive(amount, "Çekilecek");

            if (amount > Balance + _overdraftLimit)
                throw new InvalidOperationException("Kredi limiti aşıldı! Çekim iptal edildi.");

            Balance -= amount;
            _logger.Log($"{Owner} vadesiz hesabından {FormatMoney(amount)} çekildi. Kalan bakiye: {FormatMoney(Balance)}");
        }

        public override void CalculateInterest()
        {
            _logger.Log($"{Owner} vadesiz hesabı faiz getirmez.");
        }
    }
}
