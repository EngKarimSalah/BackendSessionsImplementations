using HospitalSoC.Models;
using HospitalSoC.Services;

namespace HospitalSoC.Presentations
{
    public class PatientPresentation
    {
        private PatientService patientService;

        public PatientPresentation(PatientService patientService)
        {
            this.patientService = patientService;
        }

        public void RegisterPatient()
        {
            Console.WriteLine("\n=== Register New Patient ===");

            Console.Write("Enter patient name: ");
            string name = Console.ReadLine();

            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter blood type (e.g. A+, O-): ");
            string bloodType = Console.ReadLine();

            int newId= patientService.RegisterPatient(name, age, phone, bloodType);

            Console.WriteLine($"Patient registered successfully. Assigned ID: {newId}");
        }

        public void ViewAllPatients()
        {
            Console.WriteLine("\n=== All Patients ===");

            List<Patient> patients = patientService.GetAll();

            foreach (Patient p in patients)
                Console.WriteLine($"ID: {p.patientId}  |  {p.patientName}  |  Age: {p.age}" +
                                  $"  |  Blood: {p.bloodType}  |  Phone: {p.phone}");
        }

        public void ViewPatientHistory()
        {
            Console.WriteLine("\n=== Patient Appointment History ===");

            List<Patient> patients = patientService.GetAll();
            foreach (Patient p in patients)
                Console.WriteLine($"  ID: {p.patientId}  |  {p.patientName}");

            Console.Write("Enter patient ID: ");
            int patientId = int.Parse(Console.ReadLine());

            Patient patient = patientService.GetHistory(patientId);

            Console.WriteLine($"\n{patient.patientName}  |  Age: {patient.age}  |  Blood: {patient.bloodType}");
            Console.WriteLine($"Appointments ({patient.Appointments.Count}):");

            decimal totalPaid = 0;
            foreach (Appointment a in patient.Appointments)
            {
                Console.WriteLine($"  ID: {a.appointmentId}  |  {a.appointmentDate:dd/MM/yyyy HH:mm}" +
                                  $"  |  Dr. {a.Doctor.doctorName}  |  {a.status}  |  Fee: {a.feePaid:C}");
                totalPaid += a.feePaid;
            }

            Console.WriteLine($"\n  Total paid: {totalPaid:C}");
        }
    }
}
