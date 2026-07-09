using HospitalSoC.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSoC
{
    public class HospitalContext : DbContext
    {
        public DbSet<Doctor>      Doctors      { get; set; }
        public DbSet<Patient>     Patients     { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Server=localhost;Database=HospitalSoCDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
    }
}
