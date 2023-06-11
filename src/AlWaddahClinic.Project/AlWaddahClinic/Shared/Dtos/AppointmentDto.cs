using System;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public PatientDto Patient { get; set; }
        public AppointmentStatusEnum? Status { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? FinishAt { get; set; }
    }
}   