using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IPatientsService
	{
		Task<ApiResponse<IEnumerable<PatientSummaryDto>>> GetAllPatients();
		Task<ApiResponse<PatientDto>> GetPatientById(Guid id);
		Task<ApiResponse> AddPatient(PatientCreateDto model);
		Task<ApiResponse<IEnumerable<PatientSummaryDto>>> SearchForPatients(string searchText);
		Task RemovePatient(Guid id);
		Task UpdatePatient(Guid id, PatientUpdateDto model);
	}
}

