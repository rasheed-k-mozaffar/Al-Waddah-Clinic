using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IAnalysisService
	{
		Task<ApiResponse<List<double>>> GetPatientCountByGenderAsync();
		Task<ApiResponse<List<PatientSummaryDto>>> GetRecentlyAddedPatientsAsync(int numToGet);
		Task<ApiResponse<List<AppointmentSummaryDto>>> GetUpcomingAppointmentsAsync(int limit);
	}
}

