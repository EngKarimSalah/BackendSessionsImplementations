using HospitalSoC.Models;
using HospitalSoC.Services;

namespace HospitalSoC.Presentations
{
    public class AppointmentPresentation
    {
        private AppointmentService appointmentService;
        private DoctorService      doctorService;
        private PatientService     patientService;

        public AppointmentPresentation(AppointmentService appointmentService,
                                       DoctorService doctorService,
                                       PatientService patientService)
        {
            this.appointmentService = appointmentService;
            this.doctorService      = doctorService;
            this.patientService     = patientService;
        }

        public void BookAppointment()
        {
            Console.WriteLine("\n=== Book Appointment ===");

            Console.Write("Enter patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            List<Doctor> doctors = doctorService.GetAvailable();
            Console.WriteLine("\nAvailable Doctors:");
            foreach (Doctor d in doctors)
                Console.WriteLine($"  ID: {d.doctorId}  |  {d.doctorName}  |  {d.specialization}  |  Fee: {d.consultationFee:C}");

            Console.Write("Enter doctor ID: ");
            int doctorId = int.Parse(Console.ReadLine());

            Console.Write("Enter appointment date and time (e.g. 2026-08-01 10:00): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter notes (optional — press Enter to skip): ");
            string notes = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(notes)) notes = null;

            appointmentService.BookAppointment(patientId, doctorId, date, notes);

            int newId = appointmentService.GetLastAppointmentId();
            Console.WriteLine($"Appointment booked successfully. Assigned ID: {newId}");
        }

        public void ViewAllAppointments()
        {
            Console.WriteLine("\n=== All Appointments ===");

            List<Appointment> appointments = appointmentService.GetAll();

            foreach (Appointment a in appointments)
                Console.WriteLine($"ID: {a.appointmentId}  |  {a.appointmentDate:dd/MM/yyyy HH:mm}" +
                                  $"  |  Patient: {a.Patient.patientName}" +
                                  $"  |  Doctor: {a.Doctor.doctorName}" +
                                  $"  |  Status: {a.status}  |  Fee: {a.feePaid:C}");
        }

        public void CancelAppointment()
        {
            Console.WriteLine("\n=== Cancel Appointment ===");

            List<Appointment> appointments = appointmentService.GetAll();
            foreach (Appointment a in appointments)
            {
                if (a.status == "Scheduled")
                    Console.WriteLine($"  ID: {a.appointmentId}  |  {a.appointmentDate:dd/MM/yyyy HH:mm}" +
                                      $"  |  {a.Patient.patientName}  →  Dr. {a.Doctor.doctorName}");
            }

            Console.Write("Enter appointment ID to cancel: ");
            int appointmentId = int.Parse(Console.ReadLine());

            appointmentService.CancelAppointment(appointmentId);

            Console.WriteLine("Appointment cancelled successfully.");
        }

        public void CompleteAppointment()
        {
            Console.WriteLine("\n=== Complete Appointment ===");

            List<Appointment> appointments = appointmentService.GetAll();
            foreach (Appointment a in appointments)
            {
                if (a.status == "Scheduled")
                    Console.WriteLine($"  ID: {a.appointmentId}  |  {a.appointmentDate:dd/MM/yyyy HH:mm}" +
                                      $"  |  {a.Patient.patientName}  →  Dr. {a.Doctor.doctorName}");
            }

            Console.Write("Enter appointment ID to mark as completed: ");
            int appointmentId = int.Parse(Console.ReadLine());

            appointmentService.CompleteAppointment(appointmentId);

            Console.WriteLine("Appointment marked as completed.");
        }
    }
}
