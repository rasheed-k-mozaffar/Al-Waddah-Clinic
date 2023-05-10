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

        public async Task AddPatientAsync(Patient model)
        {
            await _context.Patients.AddAsync(model);

            _context.SaveChanges();
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();

            return patients;
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if(patient == null)
            {
                throw new NotFoundException("Patient was not found");
            }

            return patient;
        }

        public async Task RemovePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                throw new NotFoundException("Patient was not found");
            }

            _context.Patients.Remove(patient);

            _context.SaveChanges();
        }
    }
}

