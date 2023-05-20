using System;
namespace AlWaddahClinic.Shared.Dtos
{
	public class NoteDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public HealthRecordDto HealthRecord { get; set; }
	}
}

			