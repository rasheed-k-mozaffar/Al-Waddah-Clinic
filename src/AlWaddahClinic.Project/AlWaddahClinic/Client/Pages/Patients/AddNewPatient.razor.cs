using AKSoftware.Localization.MultiLanguages;
using AKSoftware.Localization.MultiLanguages.Blazor;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
    public partial class AddNewPatient : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = null!;

        [Inject]
        public ILanguageContainerService Lang { get; set; } = default!;


        //list of medical cases for the Medical History
        static string[]? cases;

        private PatientCreateDto model = new();
        private IEnumerable<string>? medicalHistory;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;

        protected override void OnInitialized()
        {
            Lang.InitLocalizedComponent(this);
            cases = new string[]
            {
                Lang.Keys["MedicalCases:Smoker"], Lang.Keys["MedicalCases:Diabetes"],
                Lang.Keys["MedicalCases:BloodPressure"], Lang.Keys["MedicalCases:Cystisis"],
                Lang.Keys["MedicalCases:Asthma"], Lang.Keys["MedicalCases:Cholesterol"],
                Lang.Keys["MedicalCases:Cancer"], Lang.Keys["MedicalCases:Stroke"],
                Lang.Keys["MedicalCases:Allergies"], Lang.Keys["MedicalCases:Alcohol"],
                Lang.Keys["MedicalCases:Drugs"], Lang.Keys["MedicalCases:Anxiety"],
                Lang.Keys["MedicalCases:Depression"], Lang.Keys["MedicalCases:PrevSurgeries"],
                Lang.Keys["MedicalCases:MentalIssues"]
            };
        }

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