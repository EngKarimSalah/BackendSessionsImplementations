namespace Backend_session4.Models
{
    public class MedicalRecord
    {
        public int recordId { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public int appointmentId { get; set; }
        public string diagnosis { get; set; }
        public string prescription { get; set; }
        public string visitDate { get; set; }
        public decimal visitFee { get; set; }
    }
}
