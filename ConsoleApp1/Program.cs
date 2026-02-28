using System;
using System.Collections.Generic;

namespace EnterpriseBankingSystem
{
    
    public interface ILogger
    {
        void Log(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG - {DateTime.Now:HH:mm:ss}]: {message}");
        }
    }

    
    public abstract class BankAccount
    {
        private decimal _balance; 
        protected readonly ILogger _logger; 

        public string AccountNumber { get; }
        public string Owner { get; }

        public decimal Balance
        {
            get { return _balance; }
            protected set { _balance = value; }
        }

        protected BankAccount(string accountNumber, string owner, decimal initialBalance, ILogger logger)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = initialBalance;
            _logger = logger;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Yatırılacak tutar 0'dan büyük olmalıdır.");
            Balance += amount;
            _logger.Log($"{Owner} hesabına {amount:C2} yatırıldı. Yeni bakiye: {Balance:C2}");
        }

       
        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Çekilecek tutar 0'dan büyük olmalıdır.");
            if (amount > Balance) throw new InvalidOperationException("Yetersiz bakiye!");

            Balance -= amount;
            _logger.Log($"{Owner} hesabından {amount:C2} çekildi. Kalan bakiye: {Balance:C2}");
        }

       
        public abstract void CalculateInterest();
    }

    
    public class SavingsAccount : BankAccount
    {
        private readonly decimal _interestRate;

        public SavingsAccount(string accountNumber, string owner, decimal initialBalance, decimal interestRate, ILogger logger)
            : base(accountNumber, owner, initialBalance, logger)
        {
            _interestRate = interestRate;
        }

       
        public override void CalculateInterest()
        {
            decimal interest = Balance * _interestRate;
            Deposit(interest); 
            _logger.Log($"{Owner} hesabı faiz kazandı.");
        }
    }

   
    public class CheckingAccount : BankAccount
    {
        private readonly decimal _overdraftLimit;

        public CheckingAccount(string accountNumber, string owner, decimal initialBalance, decimal overdraftLimit, ILogger logger)
            : base(accountNumber, owner, initialBalance, logger)
        {
            _overdraftLimit = overdraftLimit;
        }

       
        public override void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Çekilecek tutar 0'dan büyük olmalıdır.");

            // Bakiye + Kredi Limiti kontrolü
            if (amount > Balance + _overdraftLimit) throw new InvalidOperationException("Kredi limiti aşıldı! Çekim iptal edildi.");

            Balance -= amount;
            _logger.Log($"{Owner} vadesiz hesabından {amount:C2} çekildi. Kalan bakiye: {Balance:C2}");
        }

        public override void CalculateInterest()
        {
            _logger.Log($"{Owner} vadesiz hesabı faiz getirmez.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();

            try
            {
                
                List<BankAccount> accounts = new List<BankAccount>
                {
                    new SavingsAccount("SA-1001", "Ahmet Yılmaz", 5000m, 0.05m, logger),
                    new CheckingAccount("CA-2001", "Ayşe Kaya", 2000m, 1000m, logger) // 1000 TL Kredi limiti var
                };

                foreach (var account in accounts)
                {
                    Console.WriteLine($"\n--- İşlemler: {account.Owner} ({account.GetType().Name}) ---");
                    account.Deposit(500m);
                    account.Withdraw(1000m);
                    account.CalculateInterest(); 
                }

                Console.WriteLine("\n--- Hata/Validasyon Testi ---");
               
                accounts[1].Withdraw(3000m);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"\nSİSTEM HATASI KORUMASI DEVREDE: {ex.Message}");
            }

            Console.WriteLine("\nProgramı kapatmak için bir tuşa basın...");
            Console.ReadLine();
        }
    }
}