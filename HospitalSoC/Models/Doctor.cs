using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSoC.Models
{
    [Table("Doctors")]
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doctorId { get; set; }                // system generated

        [Required]
        [MaxLength(100)]
        public string doctorName { get; set; }            // user input

        [Required]
        [MaxLength(100)]
        public string specialization { get; set; }        // user input

        [Required]
        [MaxLength(20)]
        public string phone { get; set; }                 // user input

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        [Range(0.01, double.MaxValue)]
        public decimal consultationFee { get; set; }      // user input

        public bool isAvailable { get; set; } = true;    // default value

        // reverse navigation — one Doctor has many Appointments
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
