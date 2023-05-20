using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
    public partial class Add : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = null!;

        private PatientCreateDto model = new();

        private string _errorMessage = string.Empty;
        private bool _isBusy = false;

        private async Task AddPatient()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                var result = await PatientsService.AddPatient(model);
                Console.WriteLine($"Name: {model.FullName} || Gender Number: {model.Gender} || Gender: {model.Gender.ToString()}");
                if (result.IsSuccess)
                {
                    await Task.Delay(1000);
                    NavigationManager.NavigateTo("/patients/all");
                }
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }
            _isBusy = false;
        }
    }
}