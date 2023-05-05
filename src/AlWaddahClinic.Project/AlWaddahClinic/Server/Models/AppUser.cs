using System;
using Microsoft.AspNetCore.Identity;

namespace AlWaddahClinic.Server.Models
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; }
		public string? ProfilePicture { get; set; }
	}
}

