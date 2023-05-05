using System;
namespace AlWaddahClinic.Server.Models
{
	public class Patient
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";	
		public int Age { get; set; }
		public string PhoneNumber { get; set; }

		//These could be null
		public string? EmaillAddress { get; set; }
		public string? Address { get; set; }
	}
}

		