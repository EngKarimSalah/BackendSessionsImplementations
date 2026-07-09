using HospitalSoC.Models;
using HospitalSoC.Services;

namespace HospitalSoC.Presentations
{
    public class DoctorPresentation
    {
        private DoctorService doctorService;

        public DoctorPresentation(DoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        public void AddDoctor()
        {
            Console.WriteLine("\n=== Add New Doctor ===");

            Console.Write("Enter doctor name: ");
            string name = Console.ReadLine();

            Console.Write("Enter specialization: ");
            string specialization = Console.ReadLine();

            Console.Write("Enter phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter consultation fee: ");
            decimal fee = decimal.Parse(Console.ReadLine());

            doctorService.AddDoctor(name, specialization, phone, fee);

            int newId = doctorService.GetLastDoctorId();
            Console.WriteLine($"Doctor added successfully. Assigned ID: {newId}");
        }

        public void ViewAllDoctors()
        {
            Console.WriteLine("\n=== All Doctors ===");

            List<Doctor> doctors = doctorService.GetAll();

            foreach (Doctor d in doctors)
                Console.WriteLine($"ID: {d.doctorId}  |  {d.doctorName}  |  {d.specialization}" +
                                  $"  |  Fee: {d.consultationFee:C}  |  Available: {d.isAvailable}");
        }

        public void ViewDoctorSchedule()
        {
            Console.WriteLine("\n=== Doctor Schedule ===");

            List<Doctor> doctors = doctorService.GetAll();
            foreach (Doctor d in doctors)
                Console.WriteLine($"  ID: {d.doctorId}  |  {d.doctorName}  |  {d.specialization}");

            Console.Write("Enter doctor ID: ");
            int doctorId = int.Parse(Console.ReadLine());

            Doctor doctor = doctorService.GetSchedule(doctorId);

            Console.WriteLine($"\nDr. {doctor.doctorName}  ({doctor.specialization})");
            Console.WriteLine($"Appointments ({doctor.Appointments.Count}):");

            foreach (Appointment a in doctor.Appointments)
                Console.WriteLine($"  ID: {a.appointmentId}  |  {a.appointmentDate:dd/MM/yyyy HH:mm}" +
                                  $"  |  Patient: {a.Patient.patientName}  |  Status: {a.status}");
        }
    }
}
