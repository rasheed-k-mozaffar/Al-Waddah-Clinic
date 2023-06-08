using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IPatientsService
	{
		Task<ApiResponse<IEnumerable<PatientSummaryDto>>> GetAllPatients();
		Task<ApiResponse<PatientDto>> GetPatientById(int id);
		Task<ApiResponse> AddPatient(PatientCreateDto model);
		Task<ApiResponse<IEnumerable<PatientSummaryDto>>> SearchForPatients(string searchText);
		Task RemovePatient(int id);
		Task UpdatePatient(int id, PatientUpdateDto model);
	}
}

