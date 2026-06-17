using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session2.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public int productStock { get; set; }
    }
}
