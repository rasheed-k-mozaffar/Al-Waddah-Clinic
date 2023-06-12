using System;
namespace AlWaddahClinic.Server.Models
{
	public class Clinic
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DoctorTypeEnum DoctorType { get; set; }
		public string? LogoUrl { get; set; }
		public string StreetAddress { get; set; }
		public string Area { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string Specialization { get; set; }
		public string Phone { get; set; }
		public string? WebsiteUrl { get; set; }
		public string DoctorName { get; set; }
		public string DoctorEmail { get; set; }
		public string StudiedAt { get; set; }
		public DateTime GraduatedIn { get; set; }
		public string? DoctorProfilePicUrl { get; set; }
	}
}

	