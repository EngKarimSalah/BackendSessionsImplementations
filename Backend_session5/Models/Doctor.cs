namespace OOP_Part3.Models
{
    public class Doctor
    {
        public int doctorId { get; set; }  //system generated
        public string doctorName { get; set; }
        public string doctorSpecialization { get; set; }
        public string doctorPhone { get; set; }
        public string doctorEmail { get; set; }
        public decimal consultationFee { get; set; }
    }
}
