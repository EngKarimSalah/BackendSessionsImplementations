using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_Solution.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }               // system generated

        [Required]
        [MaxLength(100)]
        public string categoryName { get; set; }           // user input

        [MaxLength(500)]
        public string description { get; set; }            // user input

        [MaxLength(300)]
        public string imageUrl { get; set; }               // user input
        /// ///////////////////////////////////////////////////////////////////////
   






        // reverse navigation — one Category has many Products
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
