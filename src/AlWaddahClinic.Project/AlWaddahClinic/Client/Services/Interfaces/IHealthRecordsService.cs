using System;
namespace AlWaddahClinic.Client.Services.Interfaces
{
	public interface IHealthRecordsService
	{
		Task<ApiResponse<IEnumerable<HealthRecordSummaryDto>>> GetRecordsForPatientAsync(Guid patientId);
		Task<ApiResponse<HealthRecordDto>> GetHealthRecordByIdAsync(Guid recordId);
		Task AddRecordAsync(Guid patientId, HealthRecordCreateDto model);
		Task RemoveRecordAsync(Guid recordId);
		Task UpdateRecordAsync(Guid recordId, HealthRecordUpdateDto model);
		Task MarkPaymentsCompletedAsync(Guid recordId);
    }
}

