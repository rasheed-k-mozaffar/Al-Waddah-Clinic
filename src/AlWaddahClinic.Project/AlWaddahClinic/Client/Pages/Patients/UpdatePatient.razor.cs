using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
    public partial class UpdatePatient : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = default!;

        [Parameter] public Guid PatientId { get; set; }

        //list of medical cases for the Medical History
        static string[] cases =
        {
            "Smoker", "Diabetes", "High blood pressure",
            "Cystisis", "Asthma", "High cholesterol",
            "Cancer", "Stroke", "Medication allergies",
            "Alcohol", "Drugs", "Anxiety", "Depression",
            "Previous surgeries", "Mental health issues"
        };

        private ApiResponse<PatientDto> result = new();
        private PatientUpdateDto model = new();
        private IEnumerable<string>? medicalHistory;

        private string _errorMessage = string.Empty;
        private bool _isMakingRequest = false;
        private bool _isBusy = true;
        

        protected override async Task OnInitializedAsync() {
            try {
                result = await PatientsService.GetPatientById(PatientId);
                model = MapDetails(result.Value);
                medicalHistory = model.MedicalHistory;
                
                _isBusy = false;
            }
            catch (DomainException ex) {
                _errorMessage = ex.Message;
            }
        }

        private async Task UpdateAsync() {
            _isMakingRequest = true;
            _errorMessage = string.Empty;
            try {
                //model.MedicalHistory = _medicalHistoryStr.Split(',').ToList();
                if(medicalHistory is not null && medicalHistory.Any()) {
                    model.MedicalHistory = medicalHistory.ToList();
                }
                await PatientsService.UpdatePatient(PatientId, model);
                NavigationManager.NavigateTo($"/patients/{PatientId}");
            }
            catch (DomainException ex) {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
        }

        private void GoBack() {
            NavigationManager.NavigateTo($"/patients/{PatientId}");
        }
        private PatientUpdateDto MapDetails(PatientDto patient) {
            return new PatientUpdateDto {
                FullName = patient.FullName,
                EmailAddress = patient.EmailAddress,
                DateOfBirth = patient.DateOfBirth,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
                Gender = patient.Gender,
                MedicalHistory = patient.MedicalHistory
            };
        }
    }
}

