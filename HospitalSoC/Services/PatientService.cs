using HospitalSoC.Models;
using HospitalSoC.Repositories;

namespace HospitalSoC.Services
{
    public class PatientService
    {
        private PatientRepository patientRepo;

        public PatientService(PatientRepository patientRepo)
        {
            this.patientRepo = patientRepo;
        }

        public List<Patient> GetAll()
        {
            return patientRepo.GetAll();
        }

        public Patient GetById(int patientId)
        {
            return patientRepo.GetById(patientId);
        }

        public Patient GetHistory(int patientId)
        {
            return patientRepo.GetWithAppointments(patientId);
        }

        public int RegisterPatient(string name, int age, string phone, string bloodType)
        {
            Patient patient = new Patient()
            {
                patientName = name,
                age = age,
                phone = phone,
                bloodType = bloodType,
                registrationDate = DateTime.Now
            };

            patientRepo.Add(patient);

            return patient.patientId;
        }

        public int GetLastPatientId()
        {
            List<Patient> all = patientRepo.GetAll();
            return all[all.Count - 1].patientId;
        }
    }
}
