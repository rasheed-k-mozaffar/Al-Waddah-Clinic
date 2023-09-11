using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IAnalysisRepository
	{
		Task<List<double>> CountPatientsByGenderAsync();
		Task<List<Patient>> GetRecentlyAddedPatientsAsync(int numberOfPatients);
		Task<List<Appointment>> GetUpcomingAppointmentsAsync(int hoursRange = 24);
	}
}

