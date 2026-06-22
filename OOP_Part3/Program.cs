using OOP_Part3;
using OOP_Part3.Models;
using System.Runtime.Intrinsics.X86;

namespace OOP_Part3
{
    public class Program
    {
       

        //system storage ( actual storage in memory for all lists ) 
        public static HospitalContext context = new HospitalContext
        {
            Patients = new List<Patient>(),
            Doctors = new List<Doctor>(),
            Appointments = new List<Appointment>(),
            MedicalRecords = new List<MedicalRecord>(),
            AvailableSlots = new List<AvailableSlot>()
        };
        // there are 5 empty lists exist in memory under context 


        //every user input should be validated 
        // validation types ( value correctness validation => notnull, isString )
        //                    value existance validation => right choice from showed list of options )


        // To check existance
        //      context.Availableslots.Any( s => s.Id == 5 )  ==> true or false  
          //     specific data only needed to be verified

        //     context.Availableslots.FirstOrDefault( s => s.Id == 5 )   ==> object(slot) or null
        //      whole object data is needed to be used in the upcoming steps


        // when printing /output list of options ==> showAllAvailable Slots
        //  you should check List.count > 0

        //any insertion / addition ==> must return Unique Identifier


        //Linq most common functions
        // - filtering=>       where (group satisfy condition)
        //                     firstOrDefault ( only one object satisfy condition)
        // - check existance=> Any ( true or false if object exist )
        //- 


        //Any function behaviour is Add => input collection of data of the same type (same class of affected list)
        //                              => output generated unique ID for this recently added object

        //Any function behaviour is read => read without condition ( read all ) output show all
        //                               => read with condition ( specific values / filters ) output show specifcs
        
        //Any function behavoir is update or delete => input unique ID and new value in case of user input data update
        //                                             output confirmation message 
        public static void RegisterPatient()
        {
            Console.WriteLine("\n=== Register New Patient ===");

            Console.Write("Enter patient name: ");
            string name = Console.ReadLine();

            Console.Write("Enter patient age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter patient gender (Male/Female): ");
            string gender = Console.ReadLine();

            Console.Write("Enter patient phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter patient email: ");
            string email = Console.ReadLine();

            Console.Write("Enter patient blood type (e.g. A+, O-): ");
            string bloodType = Console.ReadLine();

            int patientId = context.Patients.Count + 1;

            context.Patients.Add(

                new Patient
                {
                    patientId = patientId,
                    patientName = name,
                    patientAge = age,
                    patientGender = gender,
                    patientEmail = email,
                    patientPhone = phone,
                    patientBloodType = bloodType

                }

                );

            Console.WriteLine($"Patient registered successfully. Assigned ID: {patientId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // EASY 02 — Add a New Doctor
        // ─────────────────────────────────────────────────────────────────────
        public static void AddDoctor()
        {
            Console.WriteLine("\n=== Add New Doctor ===");

            Console.Write("Enter doctor name: ");
            string name = Console.ReadLine();

            Console.Write("Enter specialization: ");
            string specialization = Console.ReadLine();

            Console.Write("Enter doctor phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter doctor email: ");
            string email = Console.ReadLine();

            Console.Write("Enter consultation fee: ");
            decimal fee = decimal.Parse(Console.ReadLine());

            int doctorId = context.Doctors.Count + 1;

            context.Doctors.Add(new Doctor
            {
                doctorId = doctorId,
                doctorName = name,
                doctorSpecialization = specialization,
                doctorPhone = phone,
                doctorEmail = email,
                consultationFee = fee
            });

            Console.WriteLine($"Doctor added successfully. Assigned ID: {doctorId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // EASY 03 — View All Patients
        // ─────────────────────────────────────────────────────────────────────
        public static void ViewAllPatients()
        {
            Console.WriteLine("\n=== All Registered Patients ===");

            foreach (Patient p in context.Patients)
            {
                Console.WriteLine($"ID: {p.patientId}  |  Name: {p.patientName}  |  Age: {p.patientAge}" +
                                  $"  |  Gender: {p.patientGender}  |  Blood Type: {p.patientBloodType}" +
                                  $"  |  Phone: {p.patientPhone}  |  Email: {p.patientEmail}");
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // EASY 04 — View Doctors by Specialization
        // ─────────────────────────────────────────────────────────────────────
        public static void ViewDoctorsBySpecialization() //read / view
        {
            Console.WriteLine("\n=== Search Doctors by Specialization ===");

            Console.Write("Enter specialization to search: ");
            string input = Console.ReadLine().ToLower();

            List<Doctor> matched = context.Doctors
                                   .Where(d => d.doctorSpecialization.ToLower() == input)
                                   .ToList();

            foreach (Doctor d in matched)
            {
                Console.WriteLine($"ID: {d.doctorId}  |  Name: {d.doctorName}" +
                                  $"  |  Phone: {d.doctorPhone}  |  Fee: {d.consultationFee:C}");
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 05 — Add an Available Time Slot for a Doctor
        // ─────────────────────────────────────────────────────────────────────
        public static void AddAvailableSlot()
        {
            Console.WriteLine("\n=== Add Available Slot for Doctor ===");

            foreach (Doctor d in context.Doctors)
            {
                Console.WriteLine($"  ID: {d.doctorId}  |  {d.doctorName}  ({d.doctorSpecialization})");
            }

            Console.Write("Enter doctor ID: ");
            int doctorId = int.Parse(Console.ReadLine());

            bool result = context.Doctors.Any(d => d.doctorId == doctorId);
            if(result == false)
            {
                Console.WriteLine("doctor not found please insert a correct Id");
                return;
            }


            Console.Write("Enter slot date (e.g. 2026-07-10): ");
            string date = Console.ReadLine();

            Console.Write("Enter slot time (e.g. 10:00 AM): ");
            string time = Console.ReadLine();

            int slotId = context.AvailableSlots.Count + 1;

            context.AvailableSlots.Add(new AvailableSlot
            {
                slotId = slotId,
                doctorId = doctorId,
                slotDate = date,
                slotTime = time,
                isBooked = false
            });

            Console.WriteLine($"Slot added successfully with slotId: {slotId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 06 — Book an Appointment
        // ─────────────────────────────────────────────────────────────────────
        public static void BookAppointment() //create / add 
        {
            Console.WriteLine("\n=== Book an Appointment ===");

            Console.Write("Enter your patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            ViewDoctorsBySpecialization();

            Console.Write("Enter doctor ID to book with: ");
            int doctorId = int.Parse(Console.ReadLine());

            List<AvailableSlot> openSlots = context.AvailableSlots
                .Where(s => s.doctorId == doctorId && s.isBooked == false)
                .ToList();

            Console.WriteLine($"\nAvailable slots :");
            foreach (AvailableSlot s in openSlots)
            {
                Console.WriteLine($"  Slot ID: {s.slotId}  |  Date: {s.slotDate}  |  Time: {s.slotTime}");
            }

            Console.Write("Enter slot ID to book: ");
            int slotId = int.Parse(Console.ReadLine());

            AvailableSlot selectedSlot = openSlots.FirstOrDefault(s => s.slotId == slotId);

            int appointmentId = context.Appointments.Count + 1;

            context.Appointments.Add(new Appointment
            {
                appointmentId = appointmentId, //genretaed
                patientId = patientId,         // user input his number
                doctorId = doctorId,           // user input from list viewed
                slotId = slotId,               // user input from list viewed
                appointmentDate = selectedSlot.slotDate, // system calcuated 
                appointmentTime = selectedSlot.slotTime,  // system calcuated 
                status = "Scheduled"              // default value
            });

            selectedSlot.isBooked = true;  

            Console.WriteLine($"Appointment booked successfully! Appointment ID: {appointmentId}" +
                              $" | Date: {selectedSlot.slotDate} | Time: {selectedSlot.slotTime}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 07 — Cancel an Appointment
        // ─────────────────────────────────────────────────────────────────────
        public static void CancelAppointment() //edit/update data
        {
            Console.WriteLine("\n=== Cancel an Appointment ===");

            Console.Write("Enter appointment ID to cancel: ");
            int appointmentId = int.Parse(Console.ReadLine());

            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentId == appointmentId);

            AvailableSlot slot = context.AvailableSlots
                                .FirstOrDefault(s => s.slotId == appointment.slotId);

            slot.isBooked = false;

            appointment.status = "Cancelled";

            Console.WriteLine($"Appointment {appointmentId} has been cancelled and the time slot is now available again.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 08 — Create a Medical Record After a Visit
        // ─────────────────────────────────────────────────────────────────────
        public static void CreateMedicalRecord()
        {
            Console.WriteLine("\n=== Create Medical Record ===");

            Console.Write("Enter appointment ID for this visit: ");
            int appointmentId = int.Parse(Console.ReadLine());

            Console.Write("Enter diagnosis: ");
            string diagnosis = Console.ReadLine();

            Console.Write("Enter prescription / medication: ");
            string prescription = Console.ReadLine();


            Appointment appointment = context.Appointments
                          .FirstOrDefault(a => a.appointmentId == appointmentId);

            Doctor SelectedDoctor = context.Doctors.FirstOrDefault(d => d.doctorId == appointmentId);
        

        int recordId = context.MedicalRecords.Count + 1;

            context.MedicalRecords.Add(new MedicalRecord
            {
                recordId = recordId,
                appointmentId = appointmentId,
                patientId = appointment.patientId,
                doctorId = appointment.doctorId,
                visitDate = appointment.appointmentDate,
                diagnosis = diagnosis,
                prescription = prescription,
                visitFee = SelectedDoctor.consultationFee
        });

            appointment.status = "Completed";

            Console.WriteLine($"Medical record created successfully. Record ID: {recordId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // HARD 09 — Patient Medical History Report
        // ─────────────────────────────────────────────────────────────────────
        public static void PatientMedicalHistory()
        {
            Console.WriteLine("\n=== Patient Medical History Report ===");

            Console.Write("Enter patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            List<MedicalRecord> records = context.MedicalRecords
                .Where(r => r.patientId == patientId)
                .ToList();

            Console.WriteLine($"\n--- Medical History for (ID: {patientId}) ---");

            foreach (MedicalRecord r in records)
            {
                Console.WriteLine($"\n  Record ID   : {r.recordId}");
                Console.WriteLine($"  Visit Date  : {r.visitDate}");
                Console.WriteLine($"  Doctor      : {r.doctorId}");
                Console.WriteLine($"  Diagnosis   : {r.diagnosis}");
                Console.WriteLine($"  Prescription: {r.prescription}");
                Console.WriteLine($"  Fee Charged : {r.visitFee:C}");
            }

            decimal totalCharged = records.Sum(r => r.visitFee);
            Console.WriteLine($"\n  TOTAL AMOUNT CHARGED: {totalCharged:C}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // HARD 10 — Doctor Workload and Revenue Summary
        // ─────────────────────────────────────────────────────────────────────
        public static void DoctorRevenueSummary()
        {
            Console.WriteLine("\n=== Doctor Workload & Revenue Summary ===");

            var summary = context.Doctors
                .Select(d => new
                {
                    d.doctorName,
                    d.doctorSpecialization,
                    completed = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Completed"),
                    cancelled = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Cancelled"),
                    totalRevenue = context.MedicalRecords
                                       .Where(r => r.doctorId == d.doctorId)
                                       .Sum(r => r.visitFee)
                })
                .OrderByDescending(x => x.totalRevenue)
                .ToList();

            Console.WriteLine("\n  Rank  | Doctor Name               | Specialization       | Completed | Cancelled | Total Revenue");

            for (int i = 0; i < summary.Count; i++)
            {
                var x = summary[i];
                Console.WriteLine($"  #{i + 1,-5} | {x.doctorName,-25} | {x.doctorSpecialization,-20} |" +
                                  $" {x.completed,-9} | {x.cancelled,-9} | {x.totalRevenue:C}");
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // MAIN — Menu Loop
        // ─────────────────────────────────────────────────────────────────────
        static void Main(string[] args)
        {
          
            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("   Hospital Management System");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1  - Register Patient"); //Add, list(patients)
                Console.WriteLine(" 2  - Add Doctor"); // Add, list(doctors)
                Console.WriteLine(" 3  - View All Patients"); // read, List(patients)
                Console.WriteLine(" 4  - View Doctors by Specialization"); //read, List(doctors)
                Console.WriteLine(" 5  - Add Available Slot for Doctor");// Add, internal read , List(doctors, slots)
                Console.WriteLine(" 6  - Book an Appointment");// Add, internal reads and update, List ( doctor, pa, slots, appoint)
                Console.WriteLine(" 7  - Cancel an Appointment"); //update , internal read, List(appoin, slot)
                Console.WriteLine(" 8  - Create Medical Record"); //Add, internal read, List( appoint, patient)
                Console.WriteLine(" 9  - Patient Medical History Report"); //read, List(medical report)
                Console.WriteLine(" 10 - Doctor Workload & Revenue Summary"); //read, List(doctors, appoint)
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: RegisterPatient(); break;
                    case 2: AddDoctor(); break;
                    case 3: ViewAllPatients(); break;
                    case 4: ViewDoctorsBySpecialization(); break;
                    case 5: AddAvailableSlot(); break;
                    case 6: BookAppointment(); break;
                    case 7: CancelAppointment(); break;
                    case 8: CreateMedicalRecord(); break;
                    case 9: PatientMedicalHistory(); break;
                    case 10: DoctorRevenueSummary(); break;
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