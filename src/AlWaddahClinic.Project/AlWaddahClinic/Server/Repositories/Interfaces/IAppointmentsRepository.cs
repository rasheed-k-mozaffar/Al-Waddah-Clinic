using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IAppointmentsRepository
	{
		Task<IEnumerable<Appointment>> GetAppointmentsAsync();
		Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
		Task AddAppointmentAsync(int patientId, Appointment model);
		Task RemoveAppointmentAsync(int appointmentId);
	}
}

