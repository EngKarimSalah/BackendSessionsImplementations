using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session1.Models
{
    public class UserModel
    {
         public string username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } //non static members

        public static string nationality { get; set; } = "Omani";//static member

        public List<BankAccountModel> userAccounts { get; set; }
    }
}
