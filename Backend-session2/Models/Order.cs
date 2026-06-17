using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session2.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public int customerId { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice { get; set; }
    }
}
