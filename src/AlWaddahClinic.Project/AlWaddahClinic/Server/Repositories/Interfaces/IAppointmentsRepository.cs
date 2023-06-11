using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IAppointmentsRepository
	{
		Task<IEnumerable<Appointment>> GetAppointmentsAsync();
		Task<Appointment> GetAppointmentByIdAsync(Guid appointmentId);
		Task AddAppointmentAsync(Guid patientId, Appointment model);
		Task RemoveAppointmentAsync(Guid appointmentId);
	}
}

