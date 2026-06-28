using FMS_Solution.Models;
using System.Data.Common;

namespace FMS_Solution
{
    public class FlightContext : DbConnection
    {
        public List<Passenger> Passengers   { get; set; }
        public List<Pilot>     Pilots       { get; set; }
        public List<Aircraft>  Aircrafts    { get; set; }
        public List<Flight>    Flights      { get; set; }
        public List<Booking>   Bookings     { get; set; }
    }
}
