using HospitalSoC.Models;
using HospitalSoC.Repositories;

namespace HospitalSoC.Services
{
    public class AppointmentService
    {
        private AppointmentRepository appointmentRepo;
        private DoctorRepository      doctorRepo;

        public AppointmentService(AppointmentRepository appointmentRepo, DoctorRepository doctorRepo)
        {
            this.appointmentRepo = appointmentRepo;
            this.doctorRepo      = doctorRepo;
        }

        public List<Appointment> GetAll()
        {
            return appointmentRepo.GetAll();
        }

        public List<Appointment> GetByDoctor(int doctorId)
        {
            return appointmentRepo.GetByDoctor(doctorId);
        }

        public List<Appointment> GetByPatient(int patientId)
        {
            return appointmentRepo.GetByPatient(patientId);
        }

        public Appointment GetById(int appointmentId)
        {
            return appointmentRepo.GetById(appointmentId);
        }

        public void BookAppointment(int patientId, int doctorId, DateTime date, string notes)
        {
            // Business rule: copy consultation fee from the doctor at time of booking
            Doctor doctor = doctorRepo.GetById(doctorId);

            Appointment appointment = new Appointment();
            appointment.patientId        = patientId;
            appointment.doctorId         = doctorId;
            appointment.appointmentDate  = date;
            appointment.status           = "Scheduled";
            appointment.notes            = notes;
            appointment.feePaid          = doctor.consultationFee;

            appointmentRepo.Add(appointment);
        }

        public int GetLastAppointmentId()
        {
            List<Appointment> all = appointmentRepo.GetAll();
            return all[all.Count - 1].appointmentId;
        }

        public void CancelAppointment(int appointmentId)
        {
            // Business rule: set status to Cancelled, reset fee to 0
            Appointment appointment = appointmentRepo.GetById(appointmentId);
            appointment.status  = "Cancelled";
            appointment.feePaid = 0;
            appointmentRepo.Update();
        }

        public void CompleteAppointment(int appointmentId)
        {
            // Business rule: mark appointment as completed
            Appointment appointment = appointmentRepo.GetById(appointmentId);
            appointment.status = "Completed";
            appointmentRepo.Update();
        }
    }
}
