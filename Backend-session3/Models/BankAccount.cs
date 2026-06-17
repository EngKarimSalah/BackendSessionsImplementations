using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session3.Models
{
    public class BankAccount
    {
        public int AccountId { get; }
        public string HolderName { get; set; }
        public double Balance { get; set; }          // property, not field
        public string City { get; set; }
        public string AccountType { get; set; }         // "Savings" | "Current"

        public BankAccount(int id, string holder, double balance,
                           string city, string type)
        {
            AccountId = id;
            HolderName = holder;
            Balance = balance;
            City = city;
            AccountType = type;
        }

        public override string ToString() =>
            $"[{AccountId}] {HolderName,-10} | {AccountType,-8} | {City,-8} | {Balance,9:F2} OMR";

        public void convertDataToString()
        {
           Console.WriteLine( $"{AccountId} | {HolderName,-10} | {AccountType,-8} | {City,-8} | {Balance,9:F2} OMR");
        }
    }
}
