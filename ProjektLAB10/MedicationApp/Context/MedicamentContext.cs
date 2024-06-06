using MedicamentApp.Models.ContextModels;
using MedicamentApp.Models.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace MedicationApp.Context
{
    public class MedicamentContext : DbContext
    {
        public MedicamentContext() { }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;TrustServerCertificate=True");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.IdPatient);

                entity.ToTable("patient");

                entity.Property(e => e.IdPatient)
                    .HasColumnName("IdPatient");
                entity.Property(e => e.FirstName)
                    .HasColumnName("FirstName");
                entity.Property(e => e.LastName)
                    .HasColumnName("LastName");
                entity.Property(e => e.BirthDate)
                    .HasColumnName("BirthDate");
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(e => e.IdPrescription);

                entity.ToTable("prescription");

                entity.Property(e => e.IdPrescription)
                    .HasColumnName("IdPrescription");
                entity.Property(e => e.Date)
                    .HasColumnName("Date");
                entity.Property(e => e.DueDate)
                    .HasColumnName("DueDate");
                entity.Property(e => e.IdPatient)
                    .HasColumnName("IdPatient");
                entity.Property(e => e.IdDoctor)
                    .HasColumnName("IdDoctor");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.IdDoctor);

                entity.ToTable("doctor");

                entity.Property(e => e.IdDoctor)
                    .HasColumnName("IdDoctor");
                entity.Property(e => e.FirstName)
                    .HasColumnName("FirstName");
                entity.Property(e => e.LastName)
                    .HasColumnName("LastName");
                entity.Property(e => e.Email)
                    .HasColumnName("Email");
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.IdMedicament);

                entity.ToTable("medicament");

                entity.Property(e => e.IdMedicament)
                    .HasColumnName("IdMedicament");
                entity.Property(e => e.Name)
                    .HasColumnName("Name");
                entity.Property(e => e.Description)
                    .HasColumnName("Description");
                entity.Property(e => e.Type)
                    .HasColumnName("Type");
            });

            modelBuilder.Entity<Prescription_Medicament>(entity =>
            {
                entity.HasKey(e => new { e.IdMedicament, e.IdPrescription });

                entity.ToTable("prescription_medicament");

                entity.Property(e => e.Dose)
                    .HasColumnName("Dose");
                entity.Property(e => e.Details)
                    .HasColumnName("Details");

                entity.HasOne(d => d.Medicament)
                    .WithMany(p => p.Prescription_Medicaments)
                    .HasForeignKey(d => d.IdMedicament);

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.Prescription_Medicaments)
                    .HasForeignKey(d => d.IdPrescription);
            });
        }

        public async Task<Doctor> GetDoctor(int idDoctor)
        {
            var doctor = await Doctors
                .Where(e => e.IdDoctor.Equals(idDoctor))
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                throw new Exception("A doctor with such IdDoctor does not exist");
            }
            return doctor;
        }

        public async Task<Medicament> GetMedicament(int idMedicament)
        {
            var medicament = await Medicaments
                        .Where(e => e.IdMedicament.Equals(idMedicament))
                        .FirstOrDefaultAsync();

            if (medicament == null)
            {
                throw new Exception("Medicament with IdMedicament = " + idMedicament + " does not exist");
            }
            return medicament;
        }

        public async Task<Patient> GetPatient(int idPatient)
        {
            var patient = await Patients
                    .Where(e => e.IdPatient.Equals(idPatient))
                    .FirstOrDefaultAsync();

            if (patient == null)
            {
                throw new Exception("Patient does not exist");
            }
            return patient;
        }
    }
}
