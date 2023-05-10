namespace AlWaddahClinic.Server.Repositories.Interfaces
{
    public interface IPatientsRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task RemovePatient(int id);
        Task AddPatientAsync(Patient model);

        //TODO: Complete the signatures. 
    }
}