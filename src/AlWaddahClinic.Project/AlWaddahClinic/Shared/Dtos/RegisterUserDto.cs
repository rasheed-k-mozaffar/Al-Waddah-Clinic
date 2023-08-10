﻿using System;
using System.ComponentModel.DataAnnotations;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DoctorTypeEnum DoctorType { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirmation password doesn't match the password")]
        public string PasswordConfirmation { get; set; }
    }
}

