using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IHealthRecordsService
	{
		Task<ApiResponse<IEnumerable<HealthRecordSummaryDto>>> GetRecordsForPatientAsync(int patientId);
		Task<ApiResponse<HealthRecordDto>> GetHealthRecordByIdAsync(int recordId);
		Task AddRecordAsync(int patientId, HealthRecordCreateDto model);
		Task RemoveRecordAsync(int recordId);

		//TODO: Add other signatures if necessary
	}
}

