using Backend_session7;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS_Solution.Models
{

   

    public class Booking
    {
        [Key]
        public int bookingId { get; set; }       // system generated
        //[ForeignKey()]
        public int passengerId { get; set; }     // user input his Id
        public int flightId { get; set; }        // user input chosen from list of available flights

        public string seatNumber { get; set; }   // system generated
        public string bookingDate { get; set; }  // system generated ==> set to today's date
        public decimal totalPrice { get; set; }  // system calculated from flight.ticketPrice

        public bookingstatus status { get; set; }       // default value "Confirmed" ==> | "Cancelled"
    }
}

