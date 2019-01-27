namespace P01_BillsPaymentSystem.Initializer
{
    using Data;

    public static class Seeder
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            SeedUsers(context);
            SeedBankAccounts(context);
            SeedCreditCards(context);
            SeedPaymentMethods(context);
        }

        private static void SeedUsers(BillsPaymentSystemContext context)
        {
            var users = UsersGenerator.GetUsers();

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static void SeedBankAccounts(BillsPaymentSystemContext context)
        {
            var bankAccounts = BankAccountsGenerator.GetBankAccounts();

            context.BankAccounts.AddRange(bankAccounts);
            context.SaveChanges();
        }

        private static void SeedCreditCards(BillsPaymentSystemContext context)
        {
            var creditCards = CreditCardsGenerator.GetCreditCards();

            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static void SeedPaymentMethods(BillsPaymentSystemContext context)
        {
            var paymentMethods = PaymentMethodsGenerator.GetPaymentMethods();

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }
    }
}