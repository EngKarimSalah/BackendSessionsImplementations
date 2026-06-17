namespace Backend_session4.Models
{
    public class Patient
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string patientGender { get; set; }
        public string patientPhone { get; set; }
        public string patientEmail { get; set; }
        public string patientBloodType { get; set; }

        public Patient(int id, string name, int age, string gender, string phone, string email, string bloodType)
        {
            patientId = id;
            patientName = name;
            patientAge= age;
            patientGender= gender;
            patientPhone= phone;
            patientEmail= email;
            patientBloodType= bloodType;
        }
    }
}
