using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IAppointmentsService
	{
		Task<ApiResponse<IEnumerable<AppointmentSummaryDto>>> GetAllAppointmentsAsync();
		Task<ApiResponse<AppointmentDto>> GetAppointmentByIdAsync(int appointmentId);
		Task AddAppointmentAsync(int patientId, AppointmentCreateDto model);
		Task RemoveAppointmentAsync(int appointmentId);
		Task UpdateAppointmentAsync(int appointmentId, AppointmentUpdateDto model);
		Task CompleteAppointmentAsync(int appointmentId, AppointmentStatusCheckDto model);
	}
}

