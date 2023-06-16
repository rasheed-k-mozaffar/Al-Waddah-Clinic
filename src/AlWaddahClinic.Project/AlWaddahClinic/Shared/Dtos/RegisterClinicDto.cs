using System;
using System.ComponentModel.DataAnnotations;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
	public class RegisterClinicDto
	{
        [Required(ErrorMessage = "Clinic name is required")]
        public string Name { get; set; }
        public DoctorTypeEnum DoctorType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? LogoUrl { get; set; }
        [Required(ErrorMessage = "Clinic street address is required")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Clinic area is required")]
        public string Area { get; set; }
        [Required(ErrorMessage = "City name is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country name is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Medical specialization is required")]
        public string Specialization { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; }
        public string? WebsiteUrl { get; set; }
        public string DoctorName => $"{FirstName} {LastName}";
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string DoctorEmail { get; set; }
        [Required(ErrorMessage = "Place of studies is required")]
        public string StudiedAt { get; set; }
        public DateTime? GraduatedIn { get; set; }
        public string? DoctorProfilePicUrl { get; set; }

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirmation password doesn't match the password")]
        public string PasswordConfirmation { get; set; }
    }
}

