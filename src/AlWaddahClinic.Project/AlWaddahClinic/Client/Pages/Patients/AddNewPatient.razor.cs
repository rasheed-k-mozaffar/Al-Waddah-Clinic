using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
    public partial class AddNewPatient : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = null!;


        //list of medical cases for the Medical History
        static string[] cases =
        {
            "Smoker", "Diabetes", "High blood pressure",
            "Cystisis", "Asthma", "High cholesterol",
            "Cancer", "Stroke", "Medication allergies",
            "Alcohol", "Drugs", "Anxiety", "Depression",
            "Previous surgeries", "Mental health issues"
        };

        private PatientCreateDto model = new();
        private IEnumerable<string>? medicalHistory;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;

        private async Task AddPatient()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                if(medicalHistory is not null && medicalHistory.Any()) {
                    model.MedicalHistory = medicalHistory.ToList();
                }
                else {
                    model.MedicalHistory = null;
                }

                var result = await PatientsService.AddPatient(model);

                if (result.IsSuccess)
                {
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