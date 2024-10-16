using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystemAPI.Data
{
    public class HospitalDbContext: IdentityDbContext<ApplicationUser>
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
            // Seed Specialties
            modelBuilder.Entity<Speciality>().HasData(
                new Speciality { Id = 1, Name = "General" },
                new Speciality { Id = 2, Name = "Emergency" },
                new Speciality { Id = 3, Name = "Pediatrics" },
                new Speciality { Id = 4, Name = "Cardiology" },
                new Speciality { Id = 5, Name = "Orthopedics" }
            );

            // Ensure the UserName is unique
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            // Ensure the Email is unique
            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure the Doctor and Speciality relationship
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Speciality)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.SetNull);

            //appointment and patient relation
            modelBuilder.Entity<Appointment>()
        .HasOne(a => a.Patient)
        .WithMany(p => p.Appointments)
        .HasForeignKey(a => a.PatientId)
        .OnDelete(DeleteBehavior.Restrict);

            //appointment and doctor relation
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            //medical record and patient relation
            modelBuilder.Entity<MedicalRecord>()
        .HasOne(a => a.Patient)
        .WithMany(p => p.MedicalRecords)
        .HasForeignKey(a => a.PatientId)
        .OnDelete(DeleteBehavior.Restrict);

            //medical record and doctor relation
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.MedicalRecords)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

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
