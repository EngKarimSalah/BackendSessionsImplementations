using HospitalSoC.Models;
using HospitalSoC.Repositories;

namespace HospitalSoC.Services
{
    public class DoctorService
    {
        private DoctorRepository doctorRepo;

        public DoctorService(DoctorRepository doctorRepo)
        {
            this.doctorRepo = doctorRepo;
        }

        public List<Doctor> GetAll()
        {
            return doctorRepo.GetAll();
        }

        public List<Doctor> GetAvailable()
        {
            return doctorRepo.GetAvailable();
        }

        public Doctor GetById(int doctorId)
        {
            return doctorRepo.GetById(doctorId);
        }

        public Doctor GetSchedule(int doctorId)
        {
            return doctorRepo.GetWithAppointments(doctorId);
        }

        public void AddDoctor(string name, string specialization, string phone, decimal fee)
        {
            Doctor doctor = new Doctor();
            doctor.doctorName       = name;
            doctor.specialization   = specialization;
            doctor.phone            = phone;
            doctor.consultationFee  = fee;
            doctor.isAvailable      = true;

            doctorRepo.Add(doctor);
        }

        public int GetLastDoctorId()
        {
            List<Doctor> all = doctorRepo.GetAll();
            return all[all.Count - 1].doctorId;
        }
    }
}
