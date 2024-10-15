using HospitalSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystemAPI.Data
{
    public class HospitalDbContext: DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) :
            base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSchedule> DoctorsSchedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<EmergencySchedule> EmergencySchedules { get; set; }
        public DbSet<EmergencyRecord> EmergencyRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Seed Specialties
            //modelBuilder.Entity<Speciality>().HasData(
            //    new Speciality { Id = 1, Name = "General" },
            //    new Speciality { Id = 2, Name = "Emergency" },
            //    new Speciality { Id = 3, Name = "Pediatrics" },
            //    new Speciality { Id = 4, Name = "Cardiology" },
            //    new Speciality { Id = 5, Name = "Orthopedics" }
            //);

            // Seed Admins
            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, UserName = "admin_general", Email = "admin_general@gmail.com", Password = "password123" },
                new Admin { Id = 2, UserName = "admin_emergency", Email = "admin_emergency@gmail.com", Password = "password123"},
                new Admin { Id = 3, UserName = "admin_pediatrics", Email = "admin_pediatrics@gmail.com", Password = "password123"},
                new Admin { Id = 4, UserName = "admin_cardiology", Email = "admin_cardiology@gmail.com", Password = "password123" },
                new Admin { Id = 5, UserName = "admin_orthopedics", Email = "admin_orthopedics@gmail.com", Password = "password123" }
            );

            modelBuilder.Entity<Patient>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

            modelBuilder.Entity<Doctor>()
            .Property(d => d.Id)
            .ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }
    }
}
