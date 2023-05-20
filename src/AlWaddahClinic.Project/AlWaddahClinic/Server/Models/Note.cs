using System;
namespace AlWaddahClinic.Server.Models
{
	public class Note
	{
		public int Id { get; set; }
		public int HealthRecordId { get; set; }
		public string Title { get; set; }
		public virtual HealthRecord HealthRecord { get; set; }
	}
}
	