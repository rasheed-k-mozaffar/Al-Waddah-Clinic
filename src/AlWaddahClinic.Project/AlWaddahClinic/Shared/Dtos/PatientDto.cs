﻿using System;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
    public class PatientDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public List<string>? MedicalHistory { get; set; }
        public List<HealthRecordSummaryDto>? HealthRecords { get; set; }
        public List<AppointmentSummaryDto>? Appointments { get; set; }
        public GenderEnum Gender { get; set; }
    }
}

    