using System;
namespace AlWaddahClinic.Shared.Dtos
{
    public class PrescriptionDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public AppointmentDto Appointment { get; set; }
        public List<MedicationDto> Medications { get; set; }
    }
}