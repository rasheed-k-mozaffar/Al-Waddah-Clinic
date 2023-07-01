using System;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Options;

namespace AlWaddahClinic.Server.Repositories
{
    public class HealthRecordsRepository : IHealthRecordsRepository
    {
        private readonly ClinicDbContext _context;
        private readonly IdentityOptions _options;

        public HealthRecordsRepository(ClinicDbContext context, IdentityOptions options)
        {
            _context = context;
            _options = options;
        }

        public async Task<IEnumerable<HealthRecord>> GetHealthRecordsForPatientAsync(Guid patientId)
        {
            var healthRecords = await _context.HealthRecords.Where(hr => hr.PatientId == patientId && hr.ClinicId.ToString() == _options.ClinicId).ToListAsync();

            if (!healthRecords.Any())
            {
                throw new NotFoundException("No health records were found for this patient");
            }

            return healthRecords;
        }

        public async Task<HealthRecord> GetHealthRecordByIdAsync(Guid recordId)
        {
            var healthRecord = await _context.HealthRecords.FindAsync(recordId);

            if (healthRecord != null)
            {
                return healthRecord;
            }

            throw new NotFoundException("Health record was not found");
        }

        public async Task AddHealthRecordAsync(Guid patientId, HealthRecord model)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient != null)
            {
                model.Patient = patient;
                model.ClinicId = Guid.Parse(_options.ClinicId);
                await _context.HealthRecords.AddAsync(model);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Patient was not found");
            }
        }

        public async Task RemoveHealthRecord(Guid recordId)
        {
            var healthRecord = await _context.HealthRecords.FindAsync(recordId);

            var relatedNotes = await _context.Notes.Where(n => n.HealthRecordId == recordId).ToListAsync();

            if (healthRecord == null)
            {
                throw new NotFoundException("Health record was not found");
            }


            if (relatedNotes != null)
            {
                _context.Notes.RemoveRange(relatedNotes);
            }

            _context.HealthRecords.Remove(healthRecord);
            await _context.SaveChangesAsync();
        }
    }
}

