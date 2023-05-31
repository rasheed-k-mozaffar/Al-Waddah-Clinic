using System;
namespace AlWaddahClinic.Shared.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public PatientDto Patient { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? FinishAt { get; set; }
    }
}