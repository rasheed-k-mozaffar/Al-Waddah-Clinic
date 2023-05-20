using System;
namespace AlWaddahClinic.Server.Models
{
	public class Medication : Base
	{
		public int Id { get; set; }
		public int? PrescriptionId { get; set; }
		public string CommercialName { get; set; }
		public string DailyDose { get; set; }
		public DateTime FinishAt { get; set; }
		public virtual Prescription? Prescription { get; set; }

	}
}

		