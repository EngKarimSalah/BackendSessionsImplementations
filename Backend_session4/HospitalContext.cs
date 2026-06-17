using Backend_session4.Models;

namespace Backend_session4
{
    public class HospitalContext
    {
        public List<Patient>       Patients          { get; set; }
        public List<Doctor>        Doctors           { get; set; }
        public List<Appointment>   Appointments      { get; set; }
        public List<MedicalRecord> MedicalRecords    { get; set; }
        public List<AvailableSlot> AvailableSlots    { get; set; }
    }
}
