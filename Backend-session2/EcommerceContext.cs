using System;
using System.Collections.Generic;
using System.Text;
using Backend_session2.Models;

namespace Backend_session2
{
    //system storage
    public class EcommerceContext
    {
        public List<Customer> Customers { get; set; } 
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
