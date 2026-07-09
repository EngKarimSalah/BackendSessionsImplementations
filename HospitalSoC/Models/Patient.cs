using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSoC.Models
{
    [Table("Patients")]
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int patientId { get; set; }               // system generated

        [Required]
        [MaxLength(100)]
        public string patientName { get; set; }           // user input

        [Required]
        [Range(0, 150)]
        public int age { get; set; }                      // user input

        [Required]
        [MaxLength(20)]
        public string phone { get; set; }                 // user input

        [MaxLength(10)]
        public string bloodType { get; set; }             // user input

        [Required]
        public DateTime registrationDate { get; set; }    // system generated — set to DateTime.Now

        // reverse navigation — one Patient has many Appointments
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
