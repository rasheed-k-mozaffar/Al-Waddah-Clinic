using System;
namespace AlWaddahClinic.Server.Repositories.Interfaces
{
	public interface IClinicRepository
	{
		Task<bool> CreateClinicAsync(Clinic model);
		Task RemoveClinicAsync(Guid clinicId)
;	}
}

