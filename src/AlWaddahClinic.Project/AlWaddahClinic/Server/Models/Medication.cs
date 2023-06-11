using System;
namespace AlWaddahClinic.Server.Models
{
	public class Medication : Base
	{
		public Guid Id { get; set; }
		public Guid? PrescriptionId { get; set; }
		public Guid ClinicId { get; set; }
		public string CommercialName { get; set; }
		public string DailyDose { get; set; }
		public DateTime FinishAt { get; set; }
		public virtual Prescription? Prescription { get; set; }

	}
}

			