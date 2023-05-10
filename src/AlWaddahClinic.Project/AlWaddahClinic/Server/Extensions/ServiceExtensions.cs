using System;
using System.Text;
using AlWaddahClinic.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AlWaddahClinic.Server.Extensions
{
	public static class ServiceExtensions
	{
		/// <summary>
		/// This method registers the DB context to the DI container and sets up identity.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		public static void AddDbContextAndIdentity(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ClinicDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});

			//Using the app user class to extend on the identity user.
			services.AddIdentity<AppUser, IdentityRole>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<ClinicDbContext>();
		}

		public static void AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!);
			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer("Bearer", options =>
			{

				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateLifetime = true
				};
			});
		}
	}
}

