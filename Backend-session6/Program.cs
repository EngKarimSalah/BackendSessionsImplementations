using FMS_Solution.Models;

namespace FMS_Solution
{
    public class Program
    {
        public static FlightContext context = new FlightContext()
        {
            Passengers = new List<Passenger>(),
            Pilots = new List<Pilot>(),
            Aircrafts = new List<Aircraft>(),
            Flights = new List<Flight>(),
            Bookings = new List<Booking>()
        };


        // ─────────────────────────────────────────────────────────────────────
        // EASY 01 — Register a Passenger
        // ─────────────────────────────────────────────────────────────────────
        public static void RegisterPassenger()
        {
            Console.WriteLine("\n=== Register New Passenger ===");

            Console.Write("Enter passenger name: ");
            string name = Console.ReadLine();

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter passport number: ");
            string passport = Console.ReadLine();

            Console.Write("Enter nationality: ");
            string nationality = Console.ReadLine();

            int passengerId = context.Passengers.Count + 1;

            context.Passengers.Add(new Passenger
            {
                passengerId = passengerId,
                passengerName = name,
                passengerEmail = email,
                passengerPhone = phone,
                passportNumber = passport,
                nationality = nationality
            });

            Console.WriteLine($"Passenger registered successfully. Assigned ID: {passengerId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // EASY 02 — Add an Aircraft
        // ─────────────────────────────────────────────────────────────────────
        public static void AddAircraft()
        {
            Console.WriteLine("\n=== Add New Aircraft ===");

            Console.Write("Enter aircraft model (e.g. Boeing 737): ");
            string model = Console.ReadLine();

            Console.Write("Enter total seats: ");
            int seats = int.Parse(Console.ReadLine());

            int aircraftId = context.Aircrafts.Count + 1;

            context.Aircrafts.Add(new Aircraft
            {
                aircraftId = aircraftId,
                model = model,
                totalSeats = seats,
                isOperational = true
            });

            Console.WriteLine($"Aircraft added successfully. Assigned ID: {aircraftId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // EASY 03 — Register a Pilot
        // ─────────────────────────────────────────────────────────────────────
        public static void RegisterPilot()
        {
            Console.WriteLine("\n=== Register New Pilot ===");

            Console.Write("Enter pilot name: ");
            string name = Console.ReadLine();

            Console.Write("Enter phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter license number: ");
            string license = Console.ReadLine();

            Console.Write("Enter total logged flight hours: ");
            int hours = int.Parse(Console.ReadLine());

            int pilotId = context.Pilots.Count + 1;

            context.Pilots.Add(new Pilot
            {
                pilotId = pilotId,
                pilotName = name,
                pilotPhone = phone,
                licenseNumber = license,
                flightHours = hours,
                isAvailable = true
            });

            Console.WriteLine($"Pilot registered successfully. Assigned ID: {pilotId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 05 — Schedule a Flight
        // ─────────────────────────────────────────────────────────────────────
        public static void ScheduleFlight()
        {


            Console.WriteLine("\n=== Schedule New Flight ===");

            // Show operational aircraft
            List<Aircraft> AvailableAircraftList = context.Aircrafts.Where(a => a.isOperational == true).ToList();
            Console.WriteLine("Operational aircraft:");
            foreach (Aircraft a in AvailableAircraftList)
            {
                Console.WriteLine($"  ID: {a.aircraftId}  |  {a.model}  |  Seats: {a.totalSeats}");
            }
            Console.Write("Enter aircraft ID: ");
            int aircraftId = int.Parse(Console.ReadLine());

            Aircraft aircraft = context.Aircrafts.FirstOrDefault(a => a.aircraftId == aircraftId);






            Console.Write("Enter origin city/airport: ");
            string origin = Console.ReadLine();

            Console.Write("Enter destination city/airport: ");
            string destination = Console.ReadLine();

            Console.Write("Enter departure date (e.g. 2026-07-15): ");
            string date = Console.ReadLine();

            Console.Write("Enter departure time (e.g. 09:00): ");
            string time = Console.ReadLine();

            Console.Write("Enter flight duration (hours): ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Enter ticket price: ");
            decimal price = decimal.Parse(Console.ReadLine());


            int flightId = context.Flights.Count + 1;
            string flightCode = $"OA-{flightId:D3}";




            // Show available pilots
            List<Pilot> availablePilots = context.Pilots.Where(p => p.isAvailable == true).ToList();
            Console.WriteLine("\nAvailable pilots:");
            foreach (Pilot p in availablePilots)
            {
                Console.WriteLine($"  ID: {p.pilotId}  |  {p.pilotName}  |  License: {p.licenseNumber}  |  Hours: {p.flightHours}h");
            }



            bool result = false;
            int tryCount = 0;
            int pilotId;
            do
            {

                Console.Write("Enter pilot ID: ");
                pilotId = int.Parse(Console.ReadLine());

                result = context.Pilots.Any(p => p.pilotId == pilotId);

                if (result == false)
                {
                    Console.WriteLine("pilot not found, Please re-enter");
                }
                tryCount++;

            }
            while (result == false && tryCount > 4);





            context.Flights.Add(new Flight
            {
                flightId = flightId,
                flightCode = flightCode,
                aircraftId = aircraftId,
                pilotId = pilotId,
                origin = origin,
                destination = destination,
                departureDate = date,
                departureTime = time,
                flightDuration = duration,
                ticketPrice = price,
                availableSeats = aircraft.totalSeats,
                status = "Scheduled"
            });

            Console.WriteLine($"Flight scheduled successfully. Flight Code: {flightCode}  |  Seats: {aircraft.totalSeats}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 06 — Book a Flight
        // ─────────────────────────────────────────────────────────────────────
        public static void BookFlight()
        {
            Console.WriteLine("\n=== Book a Flight ===");

            Console.Write("Enter your passenger ID: ");
            int passengerId = int.Parse(Console.ReadLine());
            bool result = context.Passengers.Any(p => p.passengerId == passengerId);

            if (result == false)
            {
                Console.WriteLine("passenger is not found");
                return;
            }




            Console.Write("Enter destination to search: ");
            string dest = Console.ReadLine().Trim().ToLower();
            List<Flight> available = context.Flights
                .Where(f => f.destination.ToLower() == dest && f.status == "Scheduled" && f.availableSeats > 0)
                .ToList();

            Console.WriteLine($"\nAvailable flights to {dest}:");
            foreach (Flight f in available)
            {
                Console.WriteLine($"  Flight ID: {f.flightId}  |  Code: {f.flightCode}" +
                                  $"  |  From: {f.origin}  |  Date: {f.departureDate}  {f.departureTime}" +
                                  $"  |  Seats left: {f.availableSeats}  |  Price: {f.ticketPrice:C}");
            }
            Console.Write("Enter flight ID to book: ");
            int flightId = int.Parse(Console.ReadLine());

            Flight selectedFlight = available.FirstOrDefault(f => f.flightId == flightId);



            int bookingId = context.Bookings.Count + 1;
            string seat = $"{(bookingId % 30) + 1}{(char)('A' + (bookingId % 6))}"; //2A , 14C

            context.Bookings.Add(new Booking
            {
                bookingId = bookingId,
                passengerId = passengerId,
                flightId = flightId,
                seatNumber = seat,
                bookingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                totalPrice = selectedFlight.ticketPrice,
                status = "Confirmed"
            });

            selectedFlight.availableSeats--;

            Console.WriteLine($"Booking confirmed! Booking ID: {bookingId}  |  Seat: {seat}  |  Price: {selectedFlight.ticketPrice:C}");
        }




        // ─────────────────────────────────────────────────────────────────────
        // EASY 04 — View All Flights
        // ─────────────────────────────────────────────────────────────────────
        public static void ViewAllFlights()
        {
            Console.WriteLine("\n=== All Scheduled Flights ===");

            foreach (Flight f in context.Flights)
            {
                Console.WriteLine($"Code: {f.flightCode}  |  {f.origin} → {f.destination}" +
                                  $"  |  Date: {f.departureDate}  |  Time: {f.departureTime}" +
                                  $"  |  Duration: {f.flightDuration}h" +
                                  $"  |  Seats Available: {f.availableSeats}" +
                                  $"  |  Price: {f.ticketPrice:C}  |  Status: {f.status}");
            }
        }





        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 07 — Cancel a Booking
        // ─────────────────────────────────────────────────────────────────────
        public static void CancelBooking() // cancel someone's booking
        {
            Console.WriteLine("\n=== Cancel a Booking ===");

            Console.Write("Enter booking ID to cancel: ");
            int bookingId = int.Parse(Console.ReadLine());

            Booking booking = context.Bookings.FirstOrDefault(b => b.bookingId == bookingId);

            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == booking.flightId);

            booking.status = "Cancelled";
            flight.availableSeats++;

            Console.WriteLine($"Booking {bookingId} cancelled. Seat returned to flight {flight.flightCode}.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 08 — Depart a Flight
        // ─────────────────────────────────────────────────────────────────────
        public static void DepartFlight()
        {
            Console.WriteLine("\n=== Depart a Flight ===");


            Console.Write("Enter flight ID to depart: ");
            int flightId = int.Parse(Console.ReadLine());

            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);

            flight.status = "Departed";
            pilot.isAvailable = true;
            pilot.flightHours += flight.flightDuration;

            Console.WriteLine($"Flight {flight.flightCode} departed successfully." +
                              $"  Pilot {pilot.pilotName} flight hours updated to {pilot.flightHours}h.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // HARD 09 — Cancel a Flight
        // ─────────────────────────────────────────────────────────────────────
        public static void CancelFlight()
        {
            Console.WriteLine("\n=== Cancel a Flight ===");

            Console.Write("Enter flight ID to cancel: ");
            int flightId = int.Parse(Console.ReadLine());

            Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

            flight.status = "Cancelled";



            // Cancel all confirmed bookings on this flight
            List<Booking> affectedBookings = context.Bookings
                                            .Where(b => b.flightId == flightId)
                                            .ToList();

            foreach (Booking b in affectedBookings)
            {
                b.status = "Cancelled";
            }

            // Free the pilot
            Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);
            pilot.isAvailable = true;


            Console.WriteLine($"Flight {flight.flightCode} cancelled." +
                              $"  {affectedBookings.Count} booking(s) cancelled." +
                              $"  Pilot {pilot.pilotName} is now available.");
        }



        // ─────────────────────────────────────────────────────────────────────
        // HARD 10 — Passenger Booking History
        // ─────────────────────────────────────────────────────────────────────
        public static void PassengerBookingHistory()
        {
            Console.WriteLine("\n=== Passenger Booking History ===");

            Console.Write("Enter passenger ID: ");
            int passengerId = int.Parse(Console.ReadLine());

            Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passengerId);

            List<Booking> records = context.Bookings
                .Where(b => b.passengerId == passengerId)
                .ToList();

            Console.WriteLine($"\n--- Booking History for {passenger.passengerName} (ID: {passengerId}) ---");

            foreach (Booking b in records)
            {
                Flight flight = context.Flights.FirstOrDefault(f => f.flightId == b.flightId);

                Console.WriteLine($"\n  Booking ID  : {b.bookingId}");
                Console.WriteLine($"  Flight Code : {flight.flightCode}");
                Console.WriteLine($"  Route       : {flight.origin} → {flight.destination}");
                Console.WriteLine($"  Date        : {flight.departureDate}");
                Console.WriteLine($"  Seat        : {b.seatNumber}");
                Console.WriteLine($"  Price Paid  : {b.totalPrice:C}");
                Console.WriteLine($"  Status      : {b.status}");
                Console.WriteLine("  " + new string('-', 45));
            }

            decimal totalSpent = records
                .Where(b => b.status == "Confirmed")
                .Sum(b => b.totalPrice);

            Console.WriteLine($"\n  TOTAL SPENT (confirmed bookings): {totalSpent:C}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // HARD 11 — Flight Revenue & Load Factor Report
        // ─────────────────────────────────────────────────────────────────────
        public static void FlightRevenueReport()
        {
            Console.WriteLine("\n=== Flight Revenue & Load Factor Report ===");

            var report = context.Flights
                .Select(f => new
                {
                    f.flightCode,
                    f.origin,
                    f.destination,
                    totalSeats = context.Aircrafts
                                        .Where(a => a.aircraftId == f.aircraftId)
                                        .Select(a => a.totalSeats)
                                        .FirstOrDefault(),
                    confirmedBookings = context.Bookings
                                        .Count(b => b.flightId == f.flightId && b.status == "Confirmed"),
                    revenue = context.Bookings
                                        .Where(b => b.flightId == f.flightId && b.status == "Confirmed")
                                        .Sum(b => b.totalPrice)
                })
                .Select(x => new
                {
                    x.flightCode,
                    x.origin,
                    x.destination,
                    x.confirmedBookings,
                    x.revenue,
                    loadFactor = x.totalSeats > 0
                        ? Math.Round((double)x.confirmedBookings / x.totalSeats * 100, 1)
                        : 0.0
                })
                .OrderByDescending(x => x.revenue)
                .ToList();

            Console.WriteLine($"\n  {"Code",-10} {"Route",-30} {"Bookings",-10} {"Load %",-10} {"Revenue",-15}");
            Console.WriteLine("  " + new string('-', 75));

            foreach (var x in report)
            {
                Console.WriteLine($"  {x.flightCode,-10} {x.origin + " → " + x.destination,-30}" +
                                  $" {x.confirmedBookings,-10} {x.loadFactor,-10:F1} {x.revenue:C}");
            }

            decimal grandTotal = report.Sum(x => x.revenue);
            Console.WriteLine("  " + new string('-', 75));
            Console.WriteLine($"  GRAND TOTAL REVENUE: {grandTotal:C}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MAIN — Menu Loop
        // ─────────────────────────────────────────────────────────────────────
        static void Main(string[] args)
        {
            sedan s = new sedan(200);

            Math.Pow(2, 5);


            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("   Flight Management System");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1  - Register Passenger");
                Console.WriteLine(" 2  - Add Aircraft");
                Console.WriteLine(" 3  - Register Pilot");
                Console.WriteLine(" 4  - View All Flights");
                Console.WriteLine(" 5  - Schedule a Flight");
                Console.WriteLine(" 6  - Book a Flight");
                Console.WriteLine(" 7  - Cancel a Booking");
                Console.WriteLine(" 8  - Depart a Flight");
                Console.WriteLine(" 9  - Cancel a Flight");
                Console.WriteLine(" 10 - Passenger Booking History");
                Console.WriteLine(" 11 - Flight Revenue & Load Factor Report");
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: RegisterPassenger(); break;
                    case 2: AddAircraft(); break;
                    case 3: RegisterPilot(); break;
                    case 4: ViewAllFlights(); break;
                    case 5: ScheduleFlight(); break;
                    case 6: BookFlight(); break;
                    case 7: CancelBooking(); break;
                    case 8: DepartFlight(); break;
                    case 9: CancelFlight(); break;
                    case 10: PassengerBookingHistory(); break;
                    case 11: FlightRevenueReport(); break;
                    case 0: exit = true; break;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }

}
