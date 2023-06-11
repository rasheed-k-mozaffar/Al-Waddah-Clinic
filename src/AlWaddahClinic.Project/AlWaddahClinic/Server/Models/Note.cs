using System;
namespace AlWaddahClinic.Server.Models
{
	public class Note
	{
		public Guid Id { get; set; }
		public Guid HealthRecordId { get; set; }
		public Guid ClinicId { get; set; }
		public string Title { get; set; }
		public virtual HealthRecord HealthRecord { get; set; }
	}
}
	