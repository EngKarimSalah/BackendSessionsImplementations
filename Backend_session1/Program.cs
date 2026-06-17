using Backend_session1.Models;
using Backend_session1.Services;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace Backend_session1
{
    public class Program
    {
      
        public static void RegisterUser(BankContext context)
        {
            Console.WriteLine("Enter username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();
            Console.WriteLine("Enter email");
            string email = Console.ReadLine();
            context.users.Add(new UserModel
            {
                username = username,
                Password = password,
                Email = email,
                userAccounts = new List<BankAccountModel>() 
            });
            EmailService.SendEmail(email, "User Registration", "Thank you for registering with our banking system.");
            Console.WriteLine("User registered successfully");
        }
        public static void CreateAccount(BankContext context)
        {
            Console.WriteLine("Enter account number");
            string accountNumber = Console.ReadLine();
            Console.WriteLine("Enter account holder name");
            string accountHolderName = Console.ReadLine();
            Console.WriteLine("Enter email address");
            string emailAddress = Console.ReadLine();
            context.accounts.Add(new BankAccountModel
            {
                accountNumber = accountNumber,
                accountHolderName = accountHolderName,
                emailAddress = emailAddress,
                balance = 0
            });
            EmailService.SendEmail(emailAddress, "Account Creation", "Your account has been created successfully.");

            Console.WriteLine("Account created successfully");
        }
        public static void CreateAccountRelatedToUser(BankContext context)
        {
            Console.WriteLine("Enter account number");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("Enter user username");
            string username = Console.ReadLine();

            foreach (UserModel user in context.users)
            {
                if(user.username == username)
                {
                    user.userAccounts.Add(new BankAccountModel
                    { accountNumber = accountNumber,
                        balance = 0

                    });

                    EmailService.SendEmail(user.Email, "Account Creation", "Your account has been created successfully.");

                }
            }

            Console.WriteLine("Account created successfully");
        }
        public static void DepositAmount(BankContext context)
        {
            Console.WriteLine("Enter your account number");
            string depAccNum = Console.ReadLine(); //A16

            Console.WriteLine("Enter deposit amount");
            double depAmount = Convert.ToDouble(Console.ReadLine());

            bool accFound = false;
            foreach (BankAccountModel account in context.accounts) //search for the account in the system storage
            {
                if (account.accountNumber == depAccNum)
                {
                    accFound = true;
                    if (BankAccountServices.Deposit(account, depAmount) == true) //usage of static method without creating an instance of the class
                    {
                        Console.WriteLine("Amount deposited successfully");
                    }
                    else
                    {
                        Console.WriteLine("Failed to deposit amount");
                    }
                }
            }

            if (accFound == false)
            {
                Console.WriteLine("Account not found");
            }
        }

        public static void Main(string[] args)
        {
            BankContext context = new BankContext(); // system storage
            context.accounts = new List<BankAccountModel>();
            context.users = new List<UserModel>();

            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("Welcome to the banking system");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Register user");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("3. Deposit amount");
                Console.WriteLine("4. Withdraw amount ");
                Console.WriteLine("5. Check balance");
                Console.WriteLine("6. Exit");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        RegisterUser(context);
                        break;

                    case 2:
                        CreateAccount(context);
                        break;


                    case 3://deposite 
                        DepositAmount(context);
                        break;

                    case 6:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }


            }

        }

        public static void commentedCode()
        {

            ////Console.WriteLine("Enter amount");
            ////int amount = Convert.ToInt32(Console.ReadLine());


            ////call by value vs call by reference
            ////any stack memory variable is call by value and any heap memory variable is call by reference

            ////system storage
            //List<BankAccount> accounts = new List<BankAccount>();


            ////system objects creation
            //BankAccount b = new BankAccount("A15", 100);
            //b.accountHolderName = "John Doe";
            //b.emailAddress = "john.doe@example.com";


            //BankAccount acc1 = new BankAccount("A15", 100) //object initializer syntax
            //{
            //    accountHolderName = "John Doe",
            //    emailAddress = "john.doe@example.com"
            //};

            //BankAccount acc2 = new BankAccount("A16", 120)
            //{
            //    accountHolderName = "karim",
            //    emailAddress = "karim@gmail.com"

            //};


            ////system processing 
            //accounts.Add(b); //index 0
            //accounts.Add(acc1); // index 1
            //accounts.Add(acc2); //index 2
            //accounts.Add(
            //                 new BankAccount("A17", 150)
            //                 {
            //                     accountHolderName = "sara",
            //                     emailAddress = "sara@gmail.com"
            //                 }
            //    ); //index 3


            //foreach (BankAccount a in accounts)
            //{
            //    Console.WriteLine(a.accountHolderName); //view data / select
            //}


            //accounts.RemoveAt(1); //remove object at index 1

            //Console.WriteLine($"Number of accounts: {accounts.Count}"); // count of objects in the list

            //accounts[0].accountHolderName = "Ali"; //update data

            ////CRUD operations on the list of accounts ( create, read, update, delete)=> general name
            ////                                          Add, select,  update, delete => EFCore
            ////                                          insert, select, update, delete => DB name
            ////                                          post,   get,    put,    delete  => WebAPIs
            ////
            ////      

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            ////system storage
            //List<BankAccountModel> accountModels = new List<BankAccountModel>();
            //accountModels.Add(new BankAccountModel
            //{
            //    accountNumber = "A15",
            //    accountHolderName = "John Doe",
            //    emailAddress = "john.doe@example.com",
            //    balance = 100
            //});

            //accountModels.Add(new BankAccountModel
            //{
            //    accountNumber = "A16",
            //    accountHolderName = "karim",
            //    emailAddress = "karim@gmail.com",
            //    balance = 100

            //});


            ////services instance
            //BankAccountServices services = new BankAccountServices();

            ////processing
            //Console.WriteLine("Enter your account number");
            //string accountNumber = Console.ReadLine(); //A16

            //Console.WriteLine("Enter deposit amount");
            //double depositAmount = Convert.ToDouble(Console.ReadLine());


            //foreach (BankAccountModel account in accountModels)
            //{
            //    if (account.accountNumber == accountNumber)
            //    {
            //        services.Deposit(account, depositAmount);
            //    }
            //}


            //////////////////////////////////////////////////////////////////////////////////////////////
        }

    }

    //public class BankAccount
    //{
    //    //SOC ( seperarion of concern ) - data members and process members should be separated

    //    //data members
    //    string accountNumber { get; } // can be setted or getted by object
    //    public string accountHolderName; // cannot be setted or getted by object
    //    public string emailAddress;
    //    double balance { get; }

    //    //process members        
    //    public void Deposit(double amount)
    //    {
    //        if (amount <= 0)
    //        {
    //            Console.WriteLine("Invalid deposit amount. Please enter a positive amount.");
    //        }
    //        else
    //        {
    //            balance += amount;
    //            Console.WriteLine($"Deposited {amount}. New balance: {balance}");
    //            SendEmail($"Deposit successful. New balance: {balance}");
    //        }
    //    }
    //    public void Withdraw(double amount, string bankName)
    //    {
    //        if (bankName == "HDFC")
    //        {
    //            balance -= amount + 0.150;
    //        }
    //        else if (bankName == "SBI")
    //        {
    //            balance -= amount + 0.100;
    //        }
    //        else if (bankName == "ICICI")
    //        {
    //            balance -= amount + 0.200;
    //        }
    //        else
    //        {
    //            balance -= amount;
    //        }

    //        SendEmail($"Withdrawal successful. New balance: {balance}");
    //        //{
    //        //    Console.WriteLine("Invalid withdrawal amount. Please enter a positive amount.");
    //        //}
    //        //else if (amount > balance)
    //        //{
    //        //    Console.WriteLine("Insufficient funds. Withdrawal denied.");
    //        //}
    //        //else
    //        //{
    //        //    balance -= amount;
    //        //    Console.WriteLine("Withdrew " + amount + " New balance " + balance);
    //        //}
    //    }
    //    public void withdraw(string bankName, double amount)
    //    {
    //        balance -= amount + 0.150;
    //        SendEmail($"Withdrawal successful. New balance: {balance}");
    //    }
    //    public void SendEmail(string message)
    //    {
    //        //code to send email to account holder emailAddress
    //        /////////
    //        /////////////
    //        ////////////////
    //        ////////////////////

    //    }

    //    //constructor 
    //    public BankAccount(string accountNumber, double initialBalance)
    //    {
    //        this.accountNumber = accountNumber;
    //        balance = initialBalance;

    //    }

    //    //single resposibility principle - a class should have

    //    //public void SetAccountBalance(double amount)
    //    //{
    //    //    amount -= 5;

    //    //    if (amount <= 0)
    //    //    {
    //    //        Console.WriteLine("Invalid balance. Please enter a positive amount.");
    //    //    }
    //    //    else
    //    //    {
    //    //        balance = amount;
    //    //    }
    //    //}
    //    //public double GetAccountBalance(string password)
    //    //{
    //    //    if (password == "secret")
    //    //    {
    //    //        return balance;
    //    //    }
    //    //    else
    //    //    {
    //    //        Console.WriteLine("Invalid password. Access denied.");
    //    //        return 0;
    //    //    }
    //    //}
    //}
}
