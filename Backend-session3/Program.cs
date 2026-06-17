using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Backend_session3.Models;

namespace Backend_session3
{

    // ── Entry Point ──────────────────────────────────────────
    public class Program
    {
        static void Main(string[] args)
        {


            //List<int> grades = new List<int>();
            //grades.Add(10);
            //grades.Add(25);
            //grades.Add(13);

            //int foundedItem = 0; //external storage for founded item, same type as searching list ==> foreach type
            //bool isFound = false;

            //foreach (int item in grades)
            //{
            //    if (item == 12)
            //    {
            //        isFound = true;
            //        Console.WriteLine(item);
            //        foundedItem = item;
            //    }

            //}

            //if (isFound == false)
            //{
            //    Console.WriteLine("not found");
            //}

    /// /////////////////////////////////////////////////////////////////////////////////////
    

        //public static bool checkValid(int ID)
        // {

        //    if (ID == 188)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        // }

        //bool result = checkValid(10);


          //  int ID = 10;
          // bool result =    ID => ID == 188  ;  //Lambda expression same result as above



            // ── Shared dataset  used throughout all examples ──────
            BankContext mainContext = new BankContext();
            mainContext.BankAccounts = new List<BankAccount>() //seed data
            {
            new BankAccount(1, "Ahmed", 1850.00, "Muscat", "Savings"),//5 ==>
            new BankAccount(2, "Sara", 320.50, "Dubai", "Current"), //2
            new BankAccount(3, "Khalid", 4200.75, "Muscat", "Savings"),//6 ==>
            new BankAccount(4, "Fatima", 95.00, "Riyadh", "Current"),//8
            new BankAccount(5, "Omar", 1500.00, "Dubai", "Savings"),//3
            new BankAccount(6, "Noura", 3100.00, "Muscat", "Current"),//7 ==>
            new BankAccount(7, "Hamed", 210.00, "Amman", "Savings"),//1
            new BankAccount(8, "Lina", 2750.25, "Dubai", "Current"),//4
            };


            Console.WriteLine("════ INITIAL DATASET ════");
            PrintAccounts(mainContext.BankAccounts);

            Demo_Filtering(mainContext.BankAccounts);
            Demo_Projection(mainContext.BankAccounts);
            Demo_Ordering(mainContext.BankAccounts);
            Demo_Aggregation(mainContext.BankAccounts);
            Demo_SetOperations(mainContext.BankAccounts);
            Demo_Composition(mainContext.BankAccounts);

            Console.WriteLine("\n════ DEMO COMPLETE ════\n");

        }


        // ════════════════════════════════════════════════════════
        //  CATEGORY 1 — FILTERING
        // ════════════════════════════════════════════════════════
        static void Demo_Filtering(List<BankAccount> accountsList)
        {
            Header("CATEGORY 1 — FILTERING");

            // Where() — accounts with balance > 1 000
            List<BankAccount> richAccounts = accountsList.Where(a => a.Balance > 1000 && a.City == "Muscat").ToList();
            if (richAccounts.Count > 0)
            {
                PrintAccounts(richAccounts);

            }
            else
            {

                Console.WriteLine("no accounts found");
            }

            // Where() — Savings accounts only
            var savings = accountsList.Where(a => a.AccountType == "Savings").ToList();
            PrintAccounts(savings);


            // FirstOrDefault() — search for city 'London' (not found → null)
            BankAccount firstLondon = accountsList.FirstOrDefault(a => a.City == "London");
            if (firstLondon != null)
            {
                firstLondon.convertDataToString();
            }
            else 
            {

                Console.WriteLine("account not found");
            }


            //int Id = 0;
            //bool result = Id >= 7 ? true : false; //ternary operator

            // Console.WriteLine("  Result: " + (notFound != null ? notFound.ToString() : "null — not found"));




            // Last() — last Dubai account
            BankAccount lastDubai = accountsList.LastOrDefault(a => a.City == "Dubai");
            if (firstLondon != null)
            {
                firstLondon.convertDataToString();
            }
            else
            {

                Console.WriteLine("account not found");
            }



            // Take(3) — first 3 accounts
            var takenElements = accountsList.Take(4).ToList();
            PrintAccounts(takenElements);

            // Skip(3).Take(2) — page 2 (accounts 4–5)
            PrintAccounts(   accountsList.Skip(3).Take(2).ToList()    );


            // Any() — is there an account with balance < 100?
            bool hasLow = accountsList.Any(a => a.Balance < 100);
            Console.WriteLine($"  Has balance < 100: {hasLow}");

            // All() — do ALL accounts have balance > 0?
            bool allPositive = accountsList.All(a => a.Balance > 0);
            Console.WriteLine($"  All balances positive: {allPositive}");

        }

        // ════════════════════════════════════════════════════════
        //  CATEGORY 2 — PROJECTION
        // ════════════════════════════════════════════════════════
        static void Demo_Projection(List<BankAccount> accountsList)
        {
            Header("CATEGORY 2 — PROJECTION");

          List<string> filteredNames = accountsList.Where(item => item.Balance > 2000) // get accounts > 2000
                                                   .Select(a => a.HolderName) // get 
                                                   .ToList();



            // Select() — extract all holder names
           var names = accountsList.Select(a => a.HolderName).ToList();
      

            // Distinct() — unique cities in dataset
            List<string> cities = accountsList.Select(a => a.City)
                                              .Distinct() //remove duplications
                                              .ToList();


            cities.Contains("cairo"); // true or false 

            Console.WriteLine($"  Contains 'Amman': {cities.Contains("Amman")}");
            Console.WriteLine($"  Contains 'Tokyo': {cities.Contains("Tokyo")}");


            // Select() — project to { Name, Balance } summary
            var summaries = accountsList.Select(a => new { a.HolderName, a.Balance })
                                        .ToList();

            foreach (var s in summaries)
                Console.WriteLine($"  {s.HolderName,-10} → {s.Balance:F2}");


            // Select() — add 5% interest label
            var withInterest = accountsList
                .Select(a => new
                {
                    a.HolderName,
                    Original = a.Balance,
                    Interest = a.Balance * 0.05,
                    NewBalance = Math.Round(a.Balance * 1.05, 2)
                })
                .ToList();
            foreach (var w in withInterest)
                Console.WriteLine($"  {w.HolderName,-10} | {w.Original,9:F2} + {w.Interest,7:F2} = {w.NewBalance,9:F2}");

            // SelectMany() — flatten list of transaction lists
            var transactionHistory = new List<List<double>>
            {
                new List<double> { 200, 450, 300 },
                new List<double> { 100, 50 },
                new List<double> { 1000, 200, 750 }
            };
            var allTransactions = transactionHistory.SelectMany(t => t).ToList();
        }

        // ════════════════════════════════════════════════════════
        //  CATEGORY 3 — ORDERING
        // ════════════════════════════════════════════════════════
        static void Demo_Ordering(List<BankAccount> accountsList)
        {
            Header("CATEGORY 3 — ORDERING");

            // OrderBy() — sort by balance ascending
            PrintAccounts(accountsList.OrderBy(a => a.Balance).ToList());

            // OrderByDescending() — sort by balance descending
            PrintAccounts(accountsList.OrderByDescending(a => a.Balance).ToList());

            // OrderBy() — sort holder names A → Z
            var alpha = accountsList.OrderBy(a => a.HolderName).Select(a => a.HolderName).ToList();

            // OrderBy(City).ThenByDescending(Balance) — city α then balance ↓
            var multiSort = accountsList
                .OrderBy(a => a.City)
                .ThenByDescending(a => a.Balance)
                .ToList();
            PrintAccounts(multiSort);

        }

        // ════════════════════════════════════════════════════════
        //  CATEGORY 4 — AGGREGATION
        // ════════════════════════════════════════════════════════
        static void Demo_Aggregation(List<BankAccount> accountsList)
        {
            Header("CATEGORY 4 — AGGREGATION");

            // Count()
            Console.WriteLine($"  Total accounts      : {accountsList.Count()}");
            Console.WriteLine($"  Savings accounts    : {accountsList.Count(a => a.AccountType == "Savings")}");
            Console.WriteLine($"  Balance > 1 000     : {accountsList.Count(a => a.Balance > 1000)}");

            // Sum()
            Console.WriteLine($"  Total deposits      : {accountsList.Sum(a => a.Balance):F2} OMR");

            // Average()
            Console.WriteLine($"  Average balance     : {accountsList.Average(a => a.Balance):F2} OMR");

            // Min() / Max()
            Console.WriteLine($"  Lowest balance      : {accountsList.Min(a => a.Balance):F2} OMR");
            Console.WriteLine($"  Highest balance     : {accountsList.Max(a => a.Balance):F2} OMR");

            // Any() / All()
            Console.WriteLine($"  Any balance < 100   : {accountsList.Any(a => a.Balance < 100)}");
            Console.WriteLine($"  All balances > 50   : {accountsList.All(a => a.Balance > 50)}");
        }



        // ════════════════════════════════════════════════════════
        //  CATEGORY 5 — SET OPERATIONS
        // ════════════════════════════════════════════════════════
        static void Demo_SetOperations(List<BankAccount> accountsList)
        {
            Header("CATEGORY 5 — SET OPERATIONS");

            List<string> groupA = new List<string> { "Ahmed", "Sara", "Khalid", "Lina" };
            List<string> groupB = new List<string> { "Sara", "Omar", "Khalid", "Noura" };

            // Union() — all unique names from both groups
            Console.WriteLine("  " + string.Join(", ", groupA.Union(groupB)));

            // Intersect() — names common to both groups
            Console.WriteLine("  " + string.Join(", ", groupA.Intersect(groupB)));

            // Except() — names in A but NOT in B
            Console.WriteLine("  " + string.Join(", ", groupA.Except(groupB)));
        }


        // ════════════════════════════════════════════════════════
        //  CATEGORY 6 — COMPOSITION (Chained Queries)
        // ════════════════════════════════════════════════════════
        static void Demo_Composition(List<BankAccount> accountsList)
        {
            Header("CATEGORY 6 — COMPOSITION (Chained / Complex Queries)");

            // Top 3 Savings accounts by balance
            var top3Savings = accountsList
                .Where(a => a.AccountType == "Savings")
                .OrderByDescending(a => a.Balance)
                .Take(3)
                .Select(a => $"{a.HolderName} ({a.Balance:F2})")
                .ToList();
            top3Savings.ForEach(s => Console.WriteLine("  " + s));

            // Total deposits held in Muscat
            double muscatTotal = accountsList
                .Where(a => a.City == "Muscat")
                .Sum(a => a.Balance);
            Console.WriteLine($"  Muscat total: {muscatTotal:F2} OMR");

            // Accounts with interest > 100 OMR, sorted by interest desc
            var interestBig = accountsList
                .Select(a => new { a.HolderName, Interest = a.Balance * 0.05 })
                .Where(x => x.Interest > 100)
                .OrderByDescending(x => x.Interest)
                .ToList();
            foreach (var x in interestBig)
                Console.WriteLine($"  {x.HolderName,-10} → interest: {x.Interest:F2}");

         

            // Paging: page 2 (size 3) from accounts sorted by balance desc
            int pageSize = 3, page = 2;
            var paged = accountsList
                .OrderByDescending(a => a.Balance)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            PrintAccounts(paged);

            // Full leaderboard — ranked by balance, with tier label

        }

        // ── Helpers ─────────────────────────────────────────────
        static void Header(string title)
        {
            Console.WriteLine($"\n\n╔══════════════════════════════════════════╗");
            Console.WriteLine($"║  {title,-40}║");
            Console.WriteLine($"╚══════════════════════════════════════════╝");
        }

        static void PrintAccounts(List<BankAccount> accountsList)
        {
            foreach (var a in accountsList)
            {
                a.convertDataToString();
            }
        }


    }
}
