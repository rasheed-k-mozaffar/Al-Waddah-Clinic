using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IAppointmentsService
	{
		Task<ApiResponse<IEnumerable<AppointmentSummaryDto>>> GetAllAppointmentsAsync();
		Task<ApiResponse<AppointmentDto>> GetAppointmentByIdAsync(Guid appointmentId);
		Task AddAppointmentAsync(Guid patientId, AppointmentCreateDto model);
		Task RemoveAppointmentAsync(Guid appointmentId);
		Task UpdateAppointmentAsync(Guid appointmentId, AppointmentUpdateDto model);
		Task CompleteAppointmentAsync(Guid appointmentId, AppointmentStatusCheckDto model);
	}
}

