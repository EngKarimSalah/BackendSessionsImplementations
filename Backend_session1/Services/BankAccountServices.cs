using Backend_session1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session1.Services
{
    public class BankAccountServices
    {

        //process members        
        public static bool Deposit(BankAccountModel account, double amount)
        {
            bool flag = false;

            if (amount > 0)
            {
                account.balance += amount;
                flag = true;

            }


            return flag;            
        }
        public double checkBalance(BankAccountModel account)
        {
            return account.balance;
        }
        public void SendEmail(BankAccountModel account, string message)
        {
            //code to send email to account holder emailAddress
           // send message to account.emailAddress;
            /////////
            /////////////
            ////////////////
            ////////////////////

        }
    }
}
