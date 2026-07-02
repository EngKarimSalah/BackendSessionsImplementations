using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_Solution.Models
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reviewId { get; set; }                 // system generated

        [Required]
        [Range(1, 5)]
        public int rating { get; set; }                   // user input — 1 to 5 stars

        [MaxLength(1000)]
        public string comment { get; set; }               // user input

        [Required]
        public DateTime reviewDate { get; set; }           // system generated — set to today's date




        // foreign key — every review is written by exactly one user
        [Required]
        [ForeignKey("User")]
        public int userId { get; set; }                   // from list — from logged-in user
        public User User { get; set; }                    // navigation property



        // foreign key — every review is about exactly one product
        [Required]
        [ForeignKey("Product")]
        public int productId { get; set; }                // from list — chosen from purchased products
        public Product Product { get; set; }              // navigation property
    }
}
