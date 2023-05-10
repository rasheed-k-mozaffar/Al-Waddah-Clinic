using System;
namespace AlWaddahClinic.Server.Models
{
	public class HealthRecord : Base
	{	
		public int Id { get; set; }
		public int PatientId { get; set; }
		public string Description { get; set; }
		public virtual Patient Patient { get; set; }
		public virtual List<Medication>? Medications { get; set; }
	}
}

		