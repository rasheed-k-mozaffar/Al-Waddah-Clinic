using System;
namespace AlWaddahClinic.Server.Models
{
	public class HealthRecord : Base
	{
		public Guid Id { get; set; }
		public Guid PatientId { get; set; }
		public Guid? AppointmentId { get; set; }
		public Guid ClinicId { get; set; }

		public string Description { get; set; }

		public virtual Patient Patient { get; set; }
		public virtual Appointment? Appointment { get; set; }
		public virtual List<Note>? Notes { get; set; }
		public virtual List<Payment>? Payments { get; set; }

		public string? TeethIds { get; set; }
		public decimal? TotalPayment { get; set; }
		public string? Currency { get; set; }
		public bool IsPaymentCompleted { get; set; }

		//Ai Insights Related Properties
		public string? PatientSuggestion { get; set; }
		public string? SuggestedMedicalTests { get; set; }
		public string? MedicalCaseInsight { get; set; }
		public string? RelatedMedicalCases { get; set; }
	}
}

