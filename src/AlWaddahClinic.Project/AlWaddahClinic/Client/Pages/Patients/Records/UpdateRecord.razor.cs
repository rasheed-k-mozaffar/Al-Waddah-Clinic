using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
    public partial class UpdateRecord : ComponentBase
    {
        #region Injected Services
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IHealthRecordsService HealthRecordsService { get; set; } = default!;

        [Inject]
        public IAiService AiService { get; set; } = default!;
        #endregion

        [Parameter] public Guid Id { get; set; }

        ApiResponse<HealthRecordDto> result = new();
        private HealthRecordUpdateDto model = new();
        private List<NoteUpdateDto>? notes = new();

        private string _noteTitle = string.Empty;
        private string InsightsPannelText => _isPreparingInsights ? "Loading..." : "Get Ai Generated Insights Regarding Your Patient's Case! Just Click The Lightbulb";

        private bool _isInsightReady = false;
        private bool _isPreparingInsights = false;
        private string _errorMessage = string.Empty;
        private bool _isMakingRequest = false;
        private bool _isBusy = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                result = await HealthRecordsService.GetHealthRecordByIdAsync(Id);
                model = MapDetails(result.Value);
                _isBusy = false;
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }
        }

        private HealthRecordUpdateDto MapDetails(HealthRecordDto recordDto)
        {
            return new HealthRecordUpdateDto
            {
                Description = recordDto.Description,
                Notes = notes,
                PatientSuggestion = recordDto.PatientSuggestion,
                SuggestedMedicalTests = recordDto.SuggestedMedicalTests,
                MedicalCaseInsight = recordDto.MedicalCaseInsight,
                RelatedMedicalCases = recordDto.RelatedMedicalCases
            };
        }

        private async Task GetInsights()
        {
            CaseDto _case = new()
            {
                message = model.Description
            };
            //Pass the description from the health record to the Ai Service
            if (!string.IsNullOrEmpty(_case.message))
            {
                _isInsightReady = false;
                _isPreparingInsights = true;

                model.PatientSuggestion = (await AiService.GetSuggestionsForPatientAsync(_case)).Value.ToList();
                model.RelatedMedicalCases = (await AiService.GetRelatedMedicalCasesAsync(_case)).Value.ToList();
                model.SuggestedMedicalTests = (await AiService.GetSuggestedMedicalTestsAsync(_case)).Value.ToList();
                model.MedicalCaseInsight = (await AiService.GetCaseInsightsAsync(_case)).Value.ToList();

                _isInsightReady = true;
                _isPreparingInsights = false;
            }
        }

        private async Task UpdateAsync()
        {
            _isMakingRequest = true;

            try
            {
                await HealthRecordsService.UpdateRecordAsync(Id ,model);
                NavigationManager.NavigateTo($"/patients/{result.Value.Patient.Id}");
            }
            catch(DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
        }

        private void AddNote()
        {
            if (!string.IsNullOrEmpty(_noteTitle))
            {
                notes.Add(new NoteUpdateDto { Title = _noteTitle });
            }

            _noteTitle = string.Empty;
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo($"/patients/{result.Value.Patient.Id}");
        }
    }
}

