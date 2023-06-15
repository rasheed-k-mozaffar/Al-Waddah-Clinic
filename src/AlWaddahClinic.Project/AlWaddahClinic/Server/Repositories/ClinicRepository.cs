using System;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Extensions;

namespace AlWaddahClinic.Server.Repositories
{
	public class ClinicRepository : IClinicRepository
	{
        private readonly ClinicDbContext _context;

        public ClinicRepository(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateClinicAsync(Clinic model)
        {
            if(model == null)
            {
                throw new ArgumentNullException("Model cannot be null");
            }

            _context.Clinics.Add(model);

            //in case the clinic was not added to the database
            if (_context.ChangeTracker.HasChanges())
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new ClinicRegisterationFailedException("Something went wrong while trying to register the clinic, please try again");
            }
        }

        public Task RemoveClinicAsync(Guid clinicId)
        {
            throw new NotImplementedException();
        }
    }
}

