using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IHealthRecordsService
	{
		Task<ApiResponse<IEnumerable<HealthRecordDto>>> GetRecordsForPatientAsync(int patientId);
		Task<ApiResponse<HealthRecordDto>> GetHealthRecordByIdAsync(int patientId, int recordId);
		Task<ApiResponse> AddRecordAsync(int patientId, HealthRecordDto model);
		Task<ApiResponse> RemoveRecordAsync(int patientId, int recordId);

		//TODO: Add other signatures if necessary
	}
}

