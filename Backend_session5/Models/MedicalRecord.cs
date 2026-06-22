namespace OOP_Part3.Models
{
    public class MedicalRecord
    {
        public int recordId { get; set; } //generated
        public int appointmentId { get; set; } // user input 

        public int patientId { get; set; } // system calcualted from appointmentId
        public int doctorId { get; set; }  // system calcualted from appointmentId

        public string diagnosis { get; set; }   //user input 
        public string prescription { get; set; } //user input 

        public string visitDate { get; set; } // system calcualted from appointmentId
        public decimal visitFee { get; set; } // system calcualted from doctorId

    }
}
