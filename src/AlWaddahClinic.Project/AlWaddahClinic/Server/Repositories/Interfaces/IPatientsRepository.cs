namespace AlWaddahClinic.Server.Repositories.Interfaces
{
    public interface IPatientsRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<IEnumerable<Patient>> SearchAsync(string searchText);
        Task<Patient> GetPatientByIdAsync(Guid id);
        Task RemovePatient(Guid id);
        Task AddPatientAsync(Patient model);
        
         
    }
}