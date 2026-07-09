using HospitalSoC.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSoC.Repositories
{
    public class PatientRepository
    {
        private HospitalContext context;

        public PatientRepository(HospitalContext context)
        {
            this.context = context;
        }

        public List<Patient> GetAll()
        {
            return context.Patients.ToList();
        }

        public Patient GetById(int patientId)
        {
            return context.Patients
                .FirstOrDefault(p => p.patientId == patientId);
        }

        public Patient GetWithAppointments(int patientId)
        {
            return context.Patients
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Doctor)
                .FirstOrDefault(p => p.patientId == patientId);
        }

        public void Add(Patient patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();
        }

    }
}
