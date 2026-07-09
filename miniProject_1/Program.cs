namespace miniProject_1
{
    internal class Program
    {
        private const string Value = "\n Account Details:";
        static List<string> customerNames = new List<string>();
        static List<string> accountNumbers = new List<string>();
        static List<double> balances = new List<double>();

        static void AddAccount()
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();


            if (accountNumbers.Contains(accountNumber))
            {
                Console.WriteLine("Account number already exists!");
                return;
            }


            double deposit;

            try
            {
                Console.Write("Enter initial deposit: ");
                deposit = double.Parse(Console.ReadLine());


                if (deposit < 0)
                {
                    Console.WriteLine("Deposit cannot be less than zero");
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid amount");
                return;
            }


            customerNames.Add(name);
            accountNumbers.Add(accountNumber);
            balances.Add(deposit);


            Console.WriteLine("Account created successfully!");
        }



        static void DepositMoney()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();


            int index = accountNumbers.IndexOf(accountNumber);


            if (index == -1)
            {
                Console.WriteLine("Account not found...");
                return;
            }


            double amount;

            try
            {
                Console.Write("Enter deposit amount: ");
                amount = double.Parse(Console.ReadLine());


                if (amount <= 0)
                {
                    Console.WriteLine("Amount must be more than zero");
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid amount...");
                return;
            }


            balances[index] += amount;


            Console.WriteLine("Deposit successful...");
            Console.WriteLine("New Balance: " + balances[index]);
        }



        static void WithdrawMoney()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();


            int index = accountNumbers.IndexOf(accountNumber);


            if (index == -1)
            {
                Console.WriteLine("Account not found...");
                return;
            }


            double amount;


            try
            {
                Console.Write("Enter withdrawal amount: ");
                amount = double.Parse(Console.ReadLine());


                if (amount <= 0)
                {
                    Console.WriteLine("Amount must be more than zero");
                    return;
                }


                if (amount > balances[index])
                {
                    Console.WriteLine("Insufficient balance");
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid amount...");
                return;
            }


            balances[index] -= amount;


            Console.WriteLine("Withdrawal successful...");
            Console.WriteLine("New Balance: " + balances[index]);
        }



        static void ShowBalance()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();


            int index = accountNumbers.IndexOf(accountNumber);


            if (index == -1)
            {
                Console.WriteLine("Account not found.");
                return;
            }


            
            Console.WriteLine("Customer Name: " + customerNames[index]);
            Console.WriteLine("Account Number: " + accountNumbers[index]);
            Console.WriteLine("Balance: " + balances[index]);
        }



        static void TransferAmount()
        {
            Console.Write("Enter sender account number: ");
            string sender = Console.ReadLine();


            Console.Write("Enter receiver account number: ");
            string receiver = Console.ReadLine();


            int senderIndex = accountNumbers.IndexOf(sender);
            int receiverIndex = accountNumbers.IndexOf(receiver);


            if (senderIndex == -1 || receiverIndex == -1)
            {
                Console.WriteLine("Account not found...");
                return;
            }


            double amount;


            try
            {
                Console.Write("Enter transfer amount: ");
                amount = double.Parse(Console.ReadLine());


                if (amount <= 0)
                {
                    Console.WriteLine("Amount must be more than zero");
                    return;
                }


                if (amount > balances[senderIndex])
                {
                    Console.WriteLine("Insufficient balance...");
                    return;
                }

            }
            catch
            {
                Console.WriteLine("Invalid amount...");
                return;
            }



            balances[senderIndex] -= amount;
            balances[receiverIndex] += amount;


            Console.WriteLine("Transfer completed successfully!");
            Console.WriteLine("Sender Balance: " + balances[senderIndex]);
            Console.WriteLine("Receiver Balance: " + balances[receiverIndex]);
        }




        static void ListAllAccounts()
        {
            if (accountNumbers.Count == 0)
            {
                Console.WriteLine("No accounts found.");
                return;
            }


            Console.WriteLine(" All Accounts ");


            for (int i = 0; i < accountNumbers.Count; i++)
            {
                Console.WriteLine("Customer: " + customerNames[i]);
                Console.WriteLine("Account Number: " + accountNumbers[i]);
                Console.WriteLine("Balance: " + balances[i]);
            }
        }



        static void ApplyInterest()
        {
            double interest = 5;


            for (int i = 0; i < balances.Count; i++)
            {
                balances[i] += balances[i] * interest / 100;
            }


            Console.WriteLine("5% interest applied successfully...");


            for (int i = 0; i < balances.Count; i++)
            {
                Console.WriteLine(
                    customerNames[i] +
                    " Balance: " +
                    balances[i]);
            }
        }
        static void Main(string[] args)
        {
            bool exitApp = false;

            while (!exitApp)
            {
                Console.WriteLine("Welcome to Spark Bank ");
                Console.WriteLine("1. Add New Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Show Balance");
                Console.WriteLine("5. Transfer Amount");
                Console.WriteLine("6. List All Accounts");
                Console.WriteLine("7. Apply Interest");
                Console.WriteLine("8. Exit");

                Console.Write("Choose an option: ");

                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 8");
                    continue;
                }


                switch (choice)
                {
                    case 1:
                        AddAccount();
                        break;

                    case 2:
                        DepositMoney();
                        break;

                    case 3:
                        WithdrawMoney();
                        break;

                    case 4:
                        ShowBalance();
                        break;

                    case 5:
                        TransferAmount();
                        break;

                    case 6:
                        ListAllAccounts();
                        break;

                    case 7:
                        ApplyInterest();
                        break;

                    case 8:
                        exitApp = true;
                        Console.WriteLine("Thank you for using our banking services..");
                        break;

                    default:
                        Console.WriteLine("Invalid option...");
                        break;
                }
            }
        }


       
    }
}
