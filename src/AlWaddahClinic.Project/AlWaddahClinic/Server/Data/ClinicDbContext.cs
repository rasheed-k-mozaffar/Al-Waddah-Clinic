using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AlWaddahClinic.Server.Data
{
	public class ClinicDbContext : IdentityDbContext
	{
		private readonly IConfiguration _configuration;

		public DbSet<Clinic> Clinics { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<HealthRecord> HealthRecords { get; set; }
		public DbSet<Medication> Medications { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Payment> Payments { get; set; }

		public ClinicDbContext(DbContextOptions<ClinicDbContext> options, IConfiguration configuration) : base(options)
		{
			_configuration = configuration;
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {	
            base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			//Configuring the relationships between the data models.
			builder.Entity<AppUser>()
				.HasOne(a => a.Clinic)
				.WithOne(c => c.AppUser)
				.HasForeignKey<AppUser>( a => a.ClinicId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Appointment>()
				.HasOne(p => p.Prescription)
				.WithOne(p => p.Appointment)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Appointment>()
				.HasOne(p => p.Patient)
				.WithMany(p => p.Appointments)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Patient>()
				.HasMany(p => p.HealthRecords)
				.WithOne(p => p.Patient)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Prescription>()
				.HasMany(p => p.Medications)
				.WithOne(p => p.Prescription)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Note>()
				.HasOne(p => p.HealthRecord)
				.WithMany(p => p.Notes)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<Appointment>()
				.HasOne(p => p.HealthRecord)
				.WithOne(p => p.Appointment)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Entity<HealthRecord>()
				.HasMany(p => p.Payments)
				.WithOne(p => p.HealthRecord)
				.OnDelete(DeleteBehavior.NoAction);

			//Seeding the 2 only roles to the database.
			builder.Entity<IdentityRole>().HasData
				(
					new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
					new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" }
				);
        }
    }
}

