using HospitalSoC.Presentations;
using HospitalSoC.Repositories;
using HospitalSoC.Services;

namespace HospitalSoC
{
    public class Program
    {
        static void Main(string[] args)
        {
            // ── Manual wiring ──────────────────────────────────────────────
            HospitalContext context = new HospitalContext();

            // Repositories
            DoctorRepository      doctorRepo      = new DoctorRepository(context);
            PatientRepository     patientRepo     = new PatientRepository(context);
            AppointmentRepository appointmentRepo = new AppointmentRepository(context);

            // Services
            DoctorService      doctorService      = new DoctorService(doctorRepo);
            PatientService     patientService     = new PatientService(patientRepo);
            AppointmentService appointmentService = new AppointmentService(appointmentRepo, doctorRepo);

            // Presentations
            DoctorPresentation      doctorPresentation      = new DoctorPresentation(doctorService);
            PatientPresentation     patientPresentation     = new PatientPresentation(patientService);
            AppointmentPresentation appointmentPresentation = new AppointmentPresentation(appointmentService, doctorService, patientService);

            // ── Menu loop ──────────────────────────────────────────────────
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("       Hospital Management System");
                Console.WriteLine("========================================");
                Console.WriteLine(" --- Doctors ---");
                Console.WriteLine(" 1  - Add Doctor");
                Console.WriteLine(" 2  - View All Doctors");
                Console.WriteLine(" 3  - View Doctor Schedule");


                Console.WriteLine(" --- Patients ---");

                Console.WriteLine(" 4  - Register Patient");
                Console.WriteLine(" 5  - View All Patients");
                Console.WriteLine(" 6  - View Patient History");



                Console.WriteLine(" --- Appointments ---");
                Console.WriteLine(" 7  - Book Appointment");
                Console.WriteLine(" 8  - View All Appointments");
                Console.WriteLine(" 9  - Cancel Appointment");
                Console.WriteLine(" 10 - Complete Appointment");
                Console.WriteLine(" 0  - Exit");
                Console.WriteLine("========================================");
                Console.Write("Select option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:  doctorPresentation.AddDoctor();                       break;
                    case 2:  doctorPresentation.ViewAllDoctors();                  break;
                    case 3:  doctorPresentation.ViewDoctorSchedule();              break;
                    case 4:  patientPresentation.RegisterPatient();                break;
                    case 5:  patientPresentation.ViewAllPatients();                break;
                    case 6:  patientPresentation.ViewPatientHistory();             break;
                    case 7:  appointmentPresentation.BookAppointment();            break;
                    case 8:  appointmentPresentation.ViewAllAppointments();        break;
                    case 9:  appointmentPresentation.CancelAppointment();          break;
                    case 10: appointmentPresentation.CompleteAppointment();        break;
                    case 0:  exit = true;                                          break;
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
