using System;
using AlWaddahClinic.Server.Data;

namespace AlWaddahClinic.Server.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly ClinicDbContext _context;

        public PatientsRepository(ClinicDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();

            return patients;
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
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
            await _context.Patients.AddAsync(model);

            _context.SaveChanges();
        }

        public async Task RemovePatient(int id)
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
    }
}

