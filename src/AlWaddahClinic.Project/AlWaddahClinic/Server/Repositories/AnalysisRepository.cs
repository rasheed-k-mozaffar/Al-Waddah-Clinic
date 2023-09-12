using System;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Options;

namespace AlWaddahClinic.Server.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
	{
        private readonly ClinicDbContext _context;
        private readonly IdentityOptions _options;

        public AnalysisRepository(ClinicDbContext context, IdentityOptions options)
        {
            _context = context;
            _options = options;
        }

        public async Task<List<double>> CountPatientsByGenderAsync() {
            int total = await _context.Patients
                .Where(p => p.ClinicId.ToString() == _options.ClinicId)
                .CountAsync();
            int males = await _context.Patients
                .Where(p => p.ClinicId.ToString() == _options.ClinicId && p.Gender == GenderEnum.Male).
                CountAsync();

            return new List<double>() { males, total - males};
        }

        public async Task<List<Patient>> GetRecentlyAddedPatientsAsync(int numberOfPatients)
        {
            if(numberOfPatients <= 0) {
                throw new InvalidDataException("0 and negative numbers are invalid for this operation");
            }

            var result = await _context.Patients
                .Where(p => p.ClinicId.ToString() == _options.ClinicId)
                .OrderByDescending(p => p.CreatedOn)
                .Take(numberOfPatients)
                .ToListAsync();

            return result;
        }

        public async Task<List<Appointment>> GetUpcomingAppointmentsAsync(int hoursRange = 24)
        {
            if(hoursRange <= 0) {
                throw new InvalidDataException("0 and negative numbers are invalid for this operation");
            }
            var now = DateTime.Now;
            var limit = now.AddHours(hoursRange);

            var result = await _context.Appointments
                .Where(a => a.ClinicId.ToString() == _options.ClinicId)
                .Where(a => a.StartAt > now && a.StartAt <= limit)
                .ToListAsync();

            return result;    
        }
    }
}

