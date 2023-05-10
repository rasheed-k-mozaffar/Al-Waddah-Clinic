using System;
namespace AlWaddahClinic.Shared.Dtos
{
    public class HealthRecordDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public PatientDto Patient { get; set; }
        public List<MedicationDto>? Medications { get; set; }
    }
}