namespace FMS_Solution.Models
{
    public class Booking
    {
        public int bookingId { get; set; }       // system generated
        public int passengerId { get; set; }     // user input his Id
        public int flightId { get; set; }        // user input chosen from list of available flights

        public string seatNumber { get; set; }   // system generated
        public string bookingDate { get; set; }  // system generated ==> set to today's date
        public decimal totalPrice { get; set; }  // system calculated from flight.ticketPrice

        public string status { get; set; }       // default value "Confirmed" ==> | "Cancelled"
    }
}
