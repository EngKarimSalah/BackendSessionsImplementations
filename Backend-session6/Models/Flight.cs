namespace FMS_Solution.Models
{
    public class Flight
    {
        public int flightId { get; set; }          // system generated
        public string flightCode { get; set; }     // system generated ==> e.g. OA-001

        public int aircraftId { get; set; }        // user input chosen from list of operational aircraft
        public int pilotId { get; set; }           // user input chosen from list of available pilots

        public string origin { get; set; }         // user input
        public string destination { get; set; }    // user input
        public string departureDate { get; set; }  // user input
        public string departureTime { get; set; }  // user input
        public int flightDuration { get; set; }    // user input (hours) ==> used to update pilot flightHours on departure
        public decimal ticketPrice { get; set; }   // user input


        public int availableSeats { get; set; }    // system calculated from aircraft.totalSeats ==> decremented on booking, incremented on cancellation

        public string status { get; set; }         // default value "Scheduled" ==> | "Departed" | "Cancelled"
    }
}
