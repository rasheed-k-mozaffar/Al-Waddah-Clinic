using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IHealthRecordsService
	{
		Task<ApiResponse<IEnumerable<HealthRecordSummaryDto>>> GetRecordsForPatientAsync(int patientId);
		Task<ApiResponse<HealthRecordDto>> GetHealthRecordByIdAsync(int recordId);
		Task AddRecordAsync(int patientId, HealthRecordCreateDto model);
		Task RemoveRecordAsync(int recordId);
		Task UpdateRecordAsync(int recordId, HealthRecordUpdateDto model);
	}
}

