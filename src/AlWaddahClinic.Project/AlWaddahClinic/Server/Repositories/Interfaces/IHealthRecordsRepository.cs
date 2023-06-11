using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IHealthRecordsRepository
	{
		Task<IEnumerable<HealthRecord>> GetHealthRecordsForPatientAsync(Guid patientId);
		Task<HealthRecord> GetHealthRecordByIdAsync(Guid recordId);
		Task RemoveHealthRecord(Guid recordId);
		Task AddHealthRecordAsync(Guid patientId, HealthRecord model);
	}
}

