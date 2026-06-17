using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_session2.Models
{
    public class Review
    {
        public int reviewId { get; set; }
        public int customerId { get; set; }
        public int productId { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
    }
}
