namespace AlWaddahClinic.Server.Repositories.Interfaces
{
    public interface IPatientsRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<IEnumerable<Patient>> SearchAsync(string searchText);
        Task<Patient> GetPatientByIdAsync(int id);
        Task RemovePatient(int id);
        Task AddPatientAsync(Patient model);
        
         
    }
}