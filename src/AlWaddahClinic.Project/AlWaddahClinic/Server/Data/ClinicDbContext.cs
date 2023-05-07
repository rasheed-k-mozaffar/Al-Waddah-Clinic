using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AlWaddahClinic.Server.Data
{
	public class ClinicDbContext : IdentityDbContext
	{
		private readonly IConfiguration _configuration;

		public DbSet<Patient> Patients { get; set; }
		public DbSet<HealthRecord> HealthRecords { get; set; }
		public DbSet<Medication> Medications { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }

		public ClinicDbContext(DbContextOptions<ClinicDbContext> options, IConfiguration configuration) : base(options)
		{
			_configuration = configuration;
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			//Configuring the relationships between the data models.
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

			//Seeding the 2 only roles to the database.
			builder.Entity<IdentityRole>().HasData
				(
					new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
					new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" }
				);

			var hasher = new PasswordHasher<AppUser>();

			builder.Entity<AppUser>().HasData
				(
					//The admin user.
					new AppUser
					{
						Id = $"{_configuration["Admin:Id"]}",
						FirstName = $"{_configuration["Admin:FirstName"]}",
						LastName = $"{_configuration["Admin:LastName"]}",
                        UserName = $"{_configuration["Admin:UserName"]}",
						NormalizedUserName = $"{_configuration["Admin:UserName"]!.ToUpper()}",
						Email = $"{_configuration["Admin:UserName"]}",
						NormalizedEmail = $"{_configuration["Admin:UserName"]!.ToUpper()}",
						EmailConfirmed = true,
						PasswordHash = hasher.HashPassword(null, $"{_configuration["Admin:Password"]}"),
						SecurityStamp = string.Empty
					}
				);

			builder.Entity<IdentityUserRole<string>>().HasData
				(
					new IdentityUserRole<string> { UserId = $"{_configuration["Admin:Id"]}", RoleId = "Admin" }
				);
        }
    }
}

