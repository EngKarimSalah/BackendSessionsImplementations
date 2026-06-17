using Backend_session4;
using Backend_session4.Models;

namespace Backend_session4
{
    public class Program
    {
        // ─────────────────────────────────────────────────────────────────────
        // EASY 01 — Patient Registration
        // touches: Patients only  →  receives List<Patient>
        // ─────────────────────────────────────────────────────────────────────
        public static void RegisterPatient(List<Patient> patients)
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

            int patientId = patients.Count + 1;

            //patients.Add(new Patient
            //{
            //    patientId = patientId,
            //    patientName = name,
            //    patientAge = age,
            //    patientGender = gender,
            //    patientPhone = phone,
            //    patientEmail = email,
            //    patientBloodType = bloodType
            //});


            patients.Add(new Patient(patientId, name, age, gender, phone, email, bloodType));

            Console.WriteLine($"Patient registered successfully. Assigned ID: {patientId}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // EASY 02 — Add a New Doctor
        // touches: Doctors only  →  receives List<Doctor>
        // ─────────────────────────────────────────────────────────────────────
        public static void AddDoctor(List<Doctor> doctors)
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

            int doctorId = doctors.Count + 1;

            doctors.Add(new Doctor
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
        // touches: Patients only  →  receives List<Patient>
        // ─────────────────────────────────────────────────────────────────────
        public static void ViewAllPatients(List<Patient> patients)
        {
            Console.WriteLine("\n=== All Registered Patients ===");

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients have been registered yet.");
                return;
            }

            // LINQ: ForEach to print each patient
           foreach(Patient p in patients)
                {
                Console.WriteLine($"ID: {p.patientId}  |  Name: {p.patientName}  |  Age: {p.patientAge}" +
                                  $"  |  Gender: {p.patientGender}  |  Blood Type: {p.patientBloodType}" +
                                  $"  |  Phone: {p.patientPhone}  |  Email: {p.patientEmail}");
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // EASY 04 — View Doctors by Specialization
        // touches: Doctors only  →  receives List<Doctor>
        // ─────────────────────────────────────────────────────────────────────
        public static void ViewDoctorsBySpecialization(List<Doctor> doctors)
        {
            Console.WriteLine("\n=== Search Doctors by Specialization ===");

            Console.Write("Enter specialization to search: ");
            string input = Console.ReadLine().Trim().ToLower();

            // LINQ: Where() to filter by specialization
            List<Doctor> matched = doctors.Where(d => d.doctorSpecialization.ToLower() == input).ToList();

            if (matched.Count == 0)
            {
                Console.WriteLine($"No doctors found with specialization '{input}'.");
                return;
            }

            matched.ForEach(d =>
                Console.WriteLine($"ID: {d.doctorId}  |  Name: {d.doctorName}" +
                                  $"  |  Phone: {d.doctorPhone}  |  Fee: {d.consultationFee:C}")
            );
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 05 — Add an Available Time Slot for a Doctor
        // touches: Doctors (read) + AvailableSlots (write)  →  keeps context
        // ─────────────────────────────────────────────────────────────────────
        public static void AddAvailableSlot(HospitalContext context)
        {
            Console.WriteLine("\n=== Add Available Slot for Doctor ===");

            if (context.Doctors.Count == 0)
            {
                Console.WriteLine("No doctors in the system yet. Please add a doctor first.");
                return;
            }

            Console.WriteLine("Available doctors:");
            // LINQ: ForEach to print all doctors
            context.Doctors.ForEach(d =>
                Console.WriteLine($"  ID: {d.doctorId}  |  {d.doctorName}  ({d.doctorSpecialization})")
            );

            Console.Write("Enter doctor ID: ");
            int doctorId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find doctor by ID
            Doctor doctor = context.Doctors.FirstOrDefault(d => d.doctorId == doctorId);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
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

            Console.WriteLine($"Slot added successfully for Dr. {doctor.doctorName} on {date} at {time}.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 06 — Book an Appointment
        // touches: Patients, Doctors, AvailableSlots, Appointments  →  keeps context
        // ─────────────────────────────────────────────────────────────────────
        public static void BookAppointment(HospitalContext context)
        {
            Console.WriteLine("\n=== Book an Appointment ===");

            Console.Write("Enter your patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find patient by ID
            Patient patient = context.Patients.FirstOrDefault(p => p.patientId == patientId);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            Console.Write("Enter doctor ID to book with: ");
            int doctorId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find doctor by ID
            Doctor doctor = context.Doctors.FirstOrDefault(d => d.doctorId == doctorId);

            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }

            // LINQ: Where() to filter unbooked slots for this doctor
            List<AvailableSlot> openSlots = context.AvailableSlots
                .Where(s => s.doctorId == doctorId && s.isBooked == false)
                .ToList();

            if (openSlots.Count == 0)
            {
                Console.WriteLine("No available slots for this doctor at the moment.");
                return;
            }

            Console.WriteLine($"\nAvailable slots for Dr. {doctor.doctorName}:");
            openSlots.ForEach(s =>
                Console.WriteLine($"  Slot ID: {s.slotId}  |  Date: {s.slotDate}  |  Time: {s.slotTime}")
            );

            Console.Write("Enter slot ID to book: ");
            int slotId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to confirm chosen slot is valid and unbooked
            AvailableSlot selectedSlot = openSlots.FirstOrDefault(s => s.slotId == slotId);

            if (selectedSlot == null)
            {
                Console.WriteLine("Slot not found or already booked.");
                return;
            }

            int appointmentId = context.Appointments.Count + 1;

            context.Appointments.Add(new Appointment
            {
                appointmentId = appointmentId,
                patientId = patientId,
                doctorId = doctorId,
                appointmentDate = selectedSlot.slotDate,
                appointmentTime = selectedSlot.slotTime,
                status = "Scheduled"
            });

            selectedSlot.isBooked = true;

            Console.WriteLine($"Appointment booked successfully! Appointment ID: {appointmentId}" +
                              $" | Date: {selectedSlot.slotDate} | Time: {selectedSlot.slotTime}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 07 — Cancel an Appointment
        // touches: Appointments (read/write) + AvailableSlots (write)  →  keeps context
        // ─────────────────────────────────────────────────────────────────────
        public static void CancelAppointment(HospitalContext context)
        {
            Console.WriteLine("\n=== Cancel an Appointment ===");

            Console.Write("Enter appointment ID to cancel: ");
            int appointmentId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find the appointment
            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentId == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            if (appointment.status == "Cancelled")
            {
                Console.WriteLine("This appointment is already cancelled.");
                return;
            }

            if (appointment.status == "Completed")
            {
                Console.WriteLine("Cannot cancel a completed appointment.");
                return;
            }

            // LINQ: FirstOrDefault() to find the matching slot and free it
            AvailableSlot slot = context.AvailableSlots.FirstOrDefault(s =>
                s.doctorId == appointment.doctorId &&
                s.slotDate == appointment.appointmentDate &&
                s.slotTime == appointment.appointmentTime
            );

            if (slot != null)
                slot.isBooked = false;

            appointment.status = "Cancelled";
            Console.WriteLine($"Appointment {appointmentId} has been cancelled and the time slot is now available again.");
        }

        // ─────────────────────────────────────────────────────────────────────
        // MEDIUM 08 — Create a Medical Record After a Visit
        // touches: Appointments, Doctors, MedicalRecords  →  keeps context
        // ─────────────────────────────────────────────────────────────────────
        public static void CreateMedicalRecord(HospitalContext context)
        {
            Console.WriteLine("\n=== Create Medical Record ===");

            Console.Write("Enter appointment ID for this visit: ");
            int appointmentId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find the appointment
            Appointment appointment = context.Appointments.FirstOrDefault(a => a.appointmentId == appointmentId);

            if (appointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }

            if (appointment.status == "Cancelled")
            {
                Console.WriteLine("Cannot create a medical record for a cancelled appointment.");
                return;
            }

            if (appointment.status == "Completed")
            {
                Console.WriteLine("A medical record already exists for this appointment.");
                return;
            }

            // LINQ: FirstOrDefault() + Select() to get the doctor's consultation fee
            decimal fee = context.Doctors
                .Where(d => d.doctorId == appointment.doctorId)
                .Select(d => d.consultationFee)
                .FirstOrDefault();

            Console.Write("Enter diagnosis: ");
            string diagnosis = Console.ReadLine();

            Console.Write("Enter prescription / medication: ");
            string prescription = Console.ReadLine();

            Console.Write("Enter visit date (e.g. 2026-07-10): ");
            string visitDate = Console.ReadLine();

            int recordId = context.MedicalRecords.Count + 1;

            context.MedicalRecords.Add(new MedicalRecord
            {
                recordId = recordId,
                patientId = appointment.patientId,
                doctorId = appointment.doctorId,
                appointmentId = appointmentId,
                diagnosis = diagnosis,
                prescription = prescription,
                visitDate = visitDate,
                visitFee = fee
            });

            appointment.status = "Completed";

            Console.WriteLine($"Medical record created successfully. Record ID: {recordId}" +
                              $" | Fee charged: {fee:C}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // HARD 09 — Patient Medical History Report
        // touches: Patients, MedicalRecords, Doctors  →  keeps context
        // ─────────────────────────────────────────────────────────────────────
        public static void PatientMedicalHistory(HospitalContext context)
        {
            Console.WriteLine("\n=== Patient Medical History Report ===");

            Console.Write("Enter patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            // LINQ: FirstOrDefault() to find the patient
            Patient patient = context.Patients.FirstOrDefault(p => p.patientId == patientId);

            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            // LINQ: Where() to get all records for this patient
            List<MedicalRecord> records = context.MedicalRecords
                .Where(r => r.patientId == patientId)
                .ToList();

            if (records.Count == 0)
            {
                Console.WriteLine("No medical records found for this patient.");
                return;
            }

            Console.WriteLine($"\n--- Medical History for {patient.patientName} (ID: {patientId}) ---");

            records.ForEach(r =>
            {
                // LINQ: FirstOrDefault() + Select() to resolve doctor name
                string doctorName = context.Doctors
                    .Where(d => d.doctorId == r.doctorId)
                    .Select(d => d.doctorName)
                    .FirstOrDefault() ?? "Unknown";

                Console.WriteLine($"\n  Record ID   : {r.recordId}");
                Console.WriteLine($"  Visit Date  : {r.visitDate}");
                Console.WriteLine($"  Doctor      : {doctorName}");
                Console.WriteLine($"  Diagnosis   : {r.diagnosis}");
                Console.WriteLine($"  Prescription: {r.prescription}");
                Console.WriteLine($"  Fee Charged : {r.visitFee:C}");
                Console.WriteLine("  " + new string('-', 50));
            });

            // LINQ: Sum() to total all fees
            decimal totalCharged = records.Sum(r => r.visitFee);
            Console.WriteLine($"\n  TOTAL AMOUNT CHARGED: {totalCharged:C}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // HARD 10 — Doctor Workload and Revenue Summary
        // touches: Doctors, Appointments, MedicalRecords  →  keeps context
        // ─────────────────────────────────────────────────────────────────────
        public static void DoctorRevenueSummary(HospitalContext context)
        {
            Console.WriteLine("\n=== Doctor Workload & Revenue Summary ===");

            if (context.Appointments.Count == 0)
            {
                Console.WriteLine("No appointments have been recorded yet.");
                return;
            }

            // LINQ: Select() to project each doctor into a summary anonymous object,
            //       then OrderByDescending() to rank by total revenue
            var summary = context.Doctors
                .Select(d => new
                {
                    d.doctorId,
                    d.doctorName,
                    d.doctorSpecialization,
                    // Count() with predicate to count completed appointments
                    completed = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Completed"),
                    // Count() with predicate to count cancelled appointments
                    cancelled = context.Appointments.Count(a => a.doctorId == d.doctorId && a.status == "Cancelled"),
                    // Sum() to total revenue from medical records
                    totalRevenue = context.MedicalRecords
                        .Where(r => r.doctorId == d.doctorId)
                        .Sum(r => r.visitFee)
                })
                .OrderByDescending(x => x.totalRevenue)
                .ToList();

            Console.WriteLine("\n  Rank  | Doctor Name               | Specialization       | Completed | Cancelled | Total Revenue");
            Console.WriteLine("  " + new string('-', 95));

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
            HospitalContext context = new HospitalContext();
            context.Patients = new List<Patient>();
            context.Doctors = new List<Doctor>();
            context.Appointments = new List<Appointment>();
            context.MedicalRecords = new List<MedicalRecord>();
            context.AvailableSlots = new List<AvailableSlot>();

            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("   Hospital Management System");
                Console.WriteLine("========================================");
                Console.WriteLine(" 1  - Register Patient");
                Console.WriteLine(" 2  - Add Doctor");
                Console.WriteLine(" 3  - View All Patients");
                Console.WriteLine(" 4  - View Doctors by Specialization");
                Console.WriteLine(" 5  - Add Available Slot for Doctor");
                Console.WriteLine(" 6  - Book an Appointment");
                Console.WriteLine(" 7  - Cancel an Appointment");
                Console.WriteLine(" 8  - Create Medical Record");
                Console.WriteLine(" 9  - Patient Medical History Report");
                Console.WriteLine(" 10 - Doctor Workload & Revenue Summary");
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: RegisterPatient(context.Patients); break;
                    case 2: AddDoctor(context.Doctors); break;
                    case 3: ViewAllPatients(context.Patients); break;
                    case 4: ViewDoctorsBySpecialization(context.Doctors); break;
                    case 5: AddAvailableSlot(context); break;
                    case 6: BookAppointment(context); break;
                    case 7: CancelAppointment(context); break;
                    case 8: CreateMedicalRecord(context); break;
                    case 9: PatientMedicalHistory(context); break;
                    case 10: DoctorRevenueSummary(context); break;
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