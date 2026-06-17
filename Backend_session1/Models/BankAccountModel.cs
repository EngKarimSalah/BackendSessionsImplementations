using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session1.Models
{
    public class BankAccountModel
    {
        //data members
     public    string accountNumber { get; set; } 
      public   string accountHolderName { get; set; }
      public   string emailAddress { get; set; } 
      public   double balance { get; set; }
    }
}
