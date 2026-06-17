using Backend_session1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session1
{
    public class BankContext
    {
        public List<BankAccountModel> accounts { get; set; }
        public List<UserModel> users { get; set; }
    }
}
