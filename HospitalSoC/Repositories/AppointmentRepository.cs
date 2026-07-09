using HospitalSoC.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSoC.Repositories
{
    public class AppointmentRepository
    {
        private HospitalContext context;

        public AppointmentRepository(HospitalContext context)
        {
            this.context = context;
        }

        public List<Appointment> GetAll()
        {
            return context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToList();
        }

        public List<Appointment> GetByDoctor(int doctorId)
        {
            return context.Appointments
                .Include(a => a.Patient)
                .Where(a => a.doctorId == doctorId)
                .ToList();
        }

        public List<Appointment> GetByPatient(int patientId)
        {
            return context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.patientId == patientId)
                .ToList();
        }

        public Appointment GetById(int appointmentId)
        {
            return context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.appointmentId == appointmentId);
        }

        public void Add(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}
