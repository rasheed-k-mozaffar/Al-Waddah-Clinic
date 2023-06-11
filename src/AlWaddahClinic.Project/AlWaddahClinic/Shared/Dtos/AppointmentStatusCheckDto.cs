using System;
using AlWaddahClinic.Shared.Enums;

namespace AlWaddahClinic.Shared.Dtos
{
	public class AppointmentStatusCheckDto
	{
		public Guid PatientId { get; set; }
		public AppointmentStatusEnum Status { get; set; }
		public HealthRecordCreateDto? HealthRecordCreate { get; set; }
	}
}

		