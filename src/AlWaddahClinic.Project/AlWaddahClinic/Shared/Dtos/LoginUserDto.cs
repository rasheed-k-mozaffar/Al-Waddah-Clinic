using System;
using System.ComponentModel.DataAnnotations;

namespace AlWaddahClinic.Shared.Dtos
{
	public class LoginUserDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}

