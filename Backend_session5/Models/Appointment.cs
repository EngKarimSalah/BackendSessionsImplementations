namespace OOP_Part3.Models
{
    public class Appointment
    {
        public int appointmentId { get; set; } // system generated
        public int patientId { get; set; } //user input 
        public int slotId { get; set; } // user input choosen from List of availeslots

        public int doctorId { get; set; }        //system calculated from slotId
        public string appointmentDate { get; set; } //system calculated
        public string appointmentTime { get; set; } //system calculated

        public string status { get; set; }   // default value "Scheduled" ==> | "Completed" | "Cancelled"
    }
}
