using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class HealthRecordUpdateDto
	{
        public string Description { get; set; }
        public List<NoteUpdateDto>? Notes { get; set; }
    }
}

