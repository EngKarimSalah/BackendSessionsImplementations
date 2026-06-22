namespace OOP_Part3.Models
{
    public class AvailableSlot
    {
        public int slotId { get; set; } //system generated
        public int doctorId { get; set; } // user input choosen from list of doctors
        public string slotDate { get; set; } //user input optional vaule 
        public string slotTime { get; set; }
        public bool isBooked { get; set; }  //default value = false
    }
}
