using System;
namespace AlWaddahClinic.Shared.Dtos
{
    public class MedicationDto
    {
        public Guid Id { get; set; }
        public string CommercialName { get; set; }
        public string DailyDose { get; set; }
        public DateTime FinishAt { get; set; }
        public PrescriptionDto? Prescription { get; set; }
    }
}