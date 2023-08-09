﻿using System;
namespace AlWaddahClinic.Shared.Dtos
{
    public class HealthRecordDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public PatientDto Patient { get; set; }
        public List<NoteDto>? Notes { get; set; }
        public List<PaymentDto>? Payments { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public string[]? TeethIds { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Currency { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<string>? PatientSuggestion { get; set; }
        public List<string>? SuggestedMedicalTests { get; set; }
        public List<string>? MedicalCaseInsight { get; set; }
        public List<string>? RelatedMedicalCases { get; set; }
    }
}