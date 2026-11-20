using System.Globalization;

namespace EnterpriseBankingSystem
{
    public abstract class BankAccount
    {
        private static readonly CultureInfo TrCulture = new CultureInfo("tr-TR");

        protected readonly ILogger _logger;

        public string AccountNumber { get; }
        public string Owner { get; }
        public decimal Balance { get; protected set; }

        protected BankAccount(string accountNumber, string owner, decimal initialBalance, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Hesap numarası boş olamaz.", nameof(accountNumber));
            if (string.IsNullOrWhiteSpace(owner))
                throw new ArgumentException("Hesap sahibi boş olamaz.", nameof(owner));
            if (initialBalance < 0)
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Başlangıç bakiyesi negatif olamaz.");

            AccountNumber = accountNumber;
            Owner = owner;
            Balance = initialBalance;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual void Deposit(decimal amount)
        {
            EnsurePositive(amount, "Yatırılacak");
            Balance += amount;
            _logger.Log($"{Owner} hesabına {FormatMoney(amount)} yatırıldı. Yeni bakiye: {FormatMoney(Balance)}");
        }

        public virtual void Withdraw(decimal amount)
        {
            EnsurePositive(amount, "Çekilecek");
            if (amount > Balance) throw new InvalidOperationException("Yetersiz bakiye!");

            Balance -= amount;
            _logger.Log($"{Owner} hesabından {FormatMoney(amount)} çekildi. Kalan bakiye: {FormatMoney(Balance)}");
        }

        public abstract void CalculateInterest();

        protected static void EnsurePositive(decimal amount, string label)
        {
            if (amount <= 0) throw new ArgumentException($"{label} tutar 0'dan büyük olmalıdır.");
        }

        protected static string FormatMoney(decimal amount) => amount.ToString("C2", TrCulture);
    }
}
