
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS_Solution.Models
{
    [Table("Client")]
    public class Passenger
    {
        [Key]
        public int passengerId { get; set; }       // system generated
        
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string passengerName { get; set; }  // user input
       
        [Required]
        [StringLength(100, MinimumLength = 20)]
        public string passengerEmail { get; set; } // user input

        [Required]
        [Range (3,70)] //numbered values
        public int age { get; set; }


        [Column("phoneNumber")] 
        public string passengerPhone { get; set; } // user input

        public string passportNumber { get; set; } // user input
        public string nationality { get; set; }    // user input

        [NotMapped]
        public DateTime CreatedAt { get; set; }
    }
}
