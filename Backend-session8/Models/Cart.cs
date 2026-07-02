using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce_Solution.Models
{
    public class Cart
    {

        [Key] //by default unique and not null no need to add [Required] attribute
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartId { get; set; } // system generated

        [Required]
        public int nubmberOfItems { get; set; } // calculated

    }
}
