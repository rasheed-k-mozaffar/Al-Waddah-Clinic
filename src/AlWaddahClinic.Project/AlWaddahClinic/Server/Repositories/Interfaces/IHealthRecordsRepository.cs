using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IHealthRecordsRepository
	{
		Task<IEnumerable<HealthRecord>> GetHealthRecordsForPatientAsync(int patientId);
		Task<HealthRecord> GetHealthRecordByIdAsync(int recordId);
		Task RemoveHealthRecord(int recordId);
		Task AddHealthRecordAsync(int patientId, HealthRecord model);
	}
}

