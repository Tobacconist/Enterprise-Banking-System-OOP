namespace EnterpriseBankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ILogger logger = new ConsoleLogger();

            try
            {
                List<BankAccount> accounts = new List<BankAccount>
                {
                    new SavingsAccount("SA-1001", "Ahmet Yılmaz", 5000m, 0.05m, logger),
                    new CheckingAccount("CA-2001", "Ayşe Kaya", 2000m, 1000m, logger)
                };

                foreach (var account in accounts)
                {
                    Console.WriteLine($"\n--- İşlemler: {account.Owner} ({account.GetType().Name}) ---");
                    account.Deposit(500m);
                    account.Withdraw(1000m);
                    account.CalculateInterest();
                }

                Console.WriteLine("\n--- Hata/Validasyon Testi ---");
                try
                {
                    accounts[1].Withdraw(3000m);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Beklenen hata yakalandı: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nBeklenmeyen hata:\n{ex}");
            }

            Console.WriteLine("\nProgramı kapatmak için bir tuşa basın...");
            Console.ReadLine();
        }
    }
}
