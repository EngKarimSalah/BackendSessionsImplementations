namespace FMS_Solution.Models
{
    public class Pilot
    {
        public int pilotId { get; set; }        // system generated
        public string pilotName { get; set; }   // user input
        public string pilotPhone { get; set; }  // user input
        public string licenseNumber { get; set; } // user input
        public int flightHours { get; set; }    // user input ==> updated after each departed flight
        public bool isAvailable { get; set; }   // default value true ==> set to false when assigned to a departed flight
    }
}
