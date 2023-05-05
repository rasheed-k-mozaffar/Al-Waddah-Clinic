using System;
namespace AlWaddahClinic.Server.Models
{
	public class Medication
	{
		public int Id { get; set; }
		public string CommercialName { get; set; }
		public string DailyDose { get; set; }
		public DateTime FinishAt { get; set; }
	}
}

	