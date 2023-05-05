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

			//Seeding the 2 only roles to the database.
			builder.Entity<IdentityRole>().HasData
				(
					new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
					new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
				);

			var hasher = new PasswordHasher<AppUser>();

			builder.Entity<AppUser>().HasData
				(
					//The admin user.
					new AppUser
					{
						Id = "1",
						FullName = $"{_configuration["Admin:FullName"]}",
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
					new IdentityUserRole<string> { UserId = "1", RoleId = "1" }
				);
        }
    }
}

