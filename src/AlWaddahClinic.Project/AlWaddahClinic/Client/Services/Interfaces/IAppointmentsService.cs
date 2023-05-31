using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IAppointmentsService
	{
		Task<IEnumerable<AppointmentSummaryDto>> GetAllAppointmentsAsync();
		Task<AppointmentDto> GetAppointmentByIdAsync(int appointmentId);
		Task AddAppointmentAsync(int patientId, AppointmentCreateDto model);
		Task RemoveAppointmentAsync(int appointmentId);
		Task UpdateAppointmentAsync(int appointmentId, AppointmentUpdateDto model);
	}
}

