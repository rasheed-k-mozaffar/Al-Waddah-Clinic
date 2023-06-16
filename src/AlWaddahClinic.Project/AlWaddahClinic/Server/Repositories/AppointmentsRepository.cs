using System;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Options;

namespace AlWaddahClinic.Server.Repositories
{
	public class AppointmentsRepository : IAppointmentsRepository
	{
        private readonly ClinicDbContext _context;
        private readonly IdentityOptions _options;

        public AppointmentsRepository(ClinicDbContext context, IdentityOptions options)
        {
            _context = context;
            _options = options;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            var appointmets = await _context.Appointments.Where(a => a.ClinicId.ToString() == _options.ClinicId).ToListAsync();

            if(appointmets == null)
            {
                throw new NotFoundException("There's currently no appointments");
            }

            return appointmets;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);

            if(appointment == null)
            {
                throw new NotFoundException("Appointment was not found");
            }

            return appointment;
        }


        public async Task AddAppointmentAsync(Guid patientId, Appointment model)
        {
            var patient = await _context.Patients.FindAsync(patientId);

            if(patient == null)
            {
                throw new NotFoundException("No patient was found with the given ID");
            }

            model.Patient = patient;
            model.ClinicId = Guid.Parse(_options.ClinicId);

            await _context.Appointments.AddAsync(model);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAppointmentAsync(Guid appointmentId)
        {
            var appointmentToRemove = await _context.Appointments.FindAsync(appointmentId);

            if(appointmentToRemove == null)
            {
                throw new NotFoundException("Appointment was not found");
            }

            _context.Appointments.Remove(appointmentToRemove);
            await _context.SaveChangesAsync();
        }
    }
}

