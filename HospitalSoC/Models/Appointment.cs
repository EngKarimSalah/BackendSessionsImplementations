using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalSoC.Models
{
    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int appointmentId { get; set; }            // system generated

        [Required]
        public DateTime appointmentDate { get; set; }     // user input

        [Required]
        [MaxLength(20)]
        public string status { get; set; } = "Scheduled"; // default value — "Scheduled" | "Completed" | "Cancelled"

        [MaxLength(500)]
        public string notes { get; set; }                  // user input (optional)

        [Column(TypeName = "decimal(8,2)")]
        public decimal feePaid { get; set; }               // calculated — copied from doctor.consultationFee

        // foreign key — every appointment belongs to one Doctor
        [Required]
        [ForeignKey("Doctor")]
        public int doctorId { get; set; }                  // from list — chosen from doctors list
        public Doctor Doctor { get; set; }                 // navigation property

        // foreign key — every appointment belongs to one Patient
        [Required]
        [ForeignKey("Patient")]
        public int patientId { get; set; }                 // from list — chosen from patients list
        public Patient Patient { get; set; }               // navigation property
    }
}
