using System;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Options;

namespace AlWaddahClinic.Server.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly ClinicDbContext _context;
        private readonly IdentityOptions _options;

        public PatientsRepository(ClinicDbContext context, IdentityOptions options)
        {
            _context = context;
            _options = options;
        }


        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.Where(p => p.ClinicId.ToString() == _options.ClinicId).ToListAsync();

            return patients;
        }

        public async Task<Patient> GetPatientByIdAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                throw new NotFoundException("Patient was not found");
            }

            return patient;
        }

        public async Task AddPatientAsync(Patient model)
        {
            model.ClinicId = Guid.Parse(_options.ClinicId);
            await _context.Patients.AddAsync(model);

            _context.SaveChanges();
        }

        public async Task RemovePatient(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);


            if (patient == null)
            {
                throw new NotFoundException("Patient was not found");
            }

            var appointments = await _context.Appointments.Where(a => a.PatientId == patient.Id).ToListAsync();
            var healthRecords = await _context.HealthRecords.Where(hr => hr.PatientId == patient.Id).ToListAsync();

            foreach(var record in healthRecords)
            {
                var relatedNotes = await _context.Notes.Where(n => n.HealthRecordId == record.Id).ToListAsync();

                if(relatedNotes != null)
                {
                    _context.Notes.RemoveRange(relatedNotes);
                }
            }

            if(appointments != null)
            {
                _context.Appointments.RemoveRange(appointments);
            }

            if(healthRecords != null)
            {
                _context.HealthRecords.RemoveRange(healthRecords);
            }
            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> SearchAsync(string searchText)
        {
            var patientsSearch = from patient in _context.Patients
                                 where patient.FullName.Contains(searchText) && patient.ClinicId.ToString() == _options.ClinicId
                                 select patient;

            if(patientsSearch == null)
            {
                throw new NotFoundException("No patient was found with the given name");
            }

            return patientsSearch;
        }
    }
}

