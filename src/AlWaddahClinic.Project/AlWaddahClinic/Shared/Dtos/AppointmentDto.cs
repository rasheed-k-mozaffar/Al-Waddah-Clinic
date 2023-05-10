using System;
namespace AlWaddahClinic.Shared.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public PatientDto Patient { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? FinishAt { get; set; }
        public PrescriptionDto? Prescription { get; set; }
    }
}