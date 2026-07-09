using HospitalSoC.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSoC.Repositories
{
    public class DoctorRepository
    {
        private HospitalContext context;

        public DoctorRepository(HospitalContext context)
        {
            this.context = context;
        }

        public List<Doctor> GetAll()
        {
            return context.Doctors.ToList();
        }

        public Doctor GetById(int doctorId)
        {
            return context.Doctors
                .FirstOrDefault(d => d.doctorId == doctorId);
        }


        public void Add(Doctor doctor)
        {
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }


        public void Remove(Doctor doctor)
        {
            context.Doctors.Remove(doctor);
            context.SaveChanges();
        }




        public List<Doctor> GetAvailable()
        {
            return context.Doctors
                .Where(d => d.isAvailable == true)
                .ToList();
        }



        public Doctor GetWithAppointments(int doctorId)
        {
            return context.Doctors
                .Include(d => d.Appointments)
                    .ThenInclude(a => a.Patient)
                .FirstOrDefault(d => d.doctorId == doctorId);
        }


        public void RemoveById(int id)
        {
            var doctor = GetById(id);
            if (doctor != null)
            {
                context.Doctors.Remove(doctor);
                context.SaveChanges();
            }
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}
