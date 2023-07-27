using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Components
{
    public partial class AppointmentStatusDialog : ComponentBase
    {
        [Inject]
        public IAppointmentsService AppointmentsService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IAiService AiService { get; set; } = default!;

        [Parameter] public Guid PatientId { get; set; }
        [Parameter] public Guid AppointmentId { get; set; }

        #region Smart Insights Collections
        private List<string>? patientSuggestions = new();
        private List<string>? relatedCases = new();
        private List<string>? suggestedTests = new();
        private List<string>? caseInsight = new();
        #endregion

        private List<NoteCreateDto>? notes = new();
        private OptionHealthRecordCreateDto? recordModel = new();
        private AppointmentStatusCheckDto model = new();

        private string _noteTitle = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isMakingRequest = false;
        private string _insightsButtonTitle => _isPreparingInsights ? "Lodaing..." : "Generate Insights";
        private string InsightsPannelText => _isPreparingInsights ? "The insights should be generated in a few seconds, please wait..." : "Get Ai Generated Insights Regarding Your Patient's Case! Just Click The Lightbulb";

        private bool _isInsightReady = false;
        private bool _isPreparingInsights = false;

        private void AddNote()
        {
            if (!string.IsNullOrEmpty(_noteTitle))
            {
                notes.Add(new NoteCreateDto { Title = _noteTitle });
            }

            _noteTitle = string.Empty;
        }


        private async Task Submit()
        {
            _isMakingRequest = true;
            _errorMessage = string.Empty;
            try
            {
                model.PatientId = PatientId;
                if(recordModel != null)
                {

                    model.HealthRecordCreate = recordModel;
                    model.HealthRecordCreate.Notes = notes;
                }
                model.HealthRecordCreate.PatientSuggestion = patientSuggestions;
                model.HealthRecordCreate.RelatedMedicalCases = relatedCases;
                model.HealthRecordCreate.MedicalCaseInsight = caseInsight;
                model.HealthRecordCreate.SuggestedMedicalTests = suggestedTests;
                await AppointmentsService.CompleteAppointmentAsync(AppointmentId, model);
                NavigationManager.NavigateTo("/appointments", true);
            }
            catch(DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
            MudDialog.Close(DialogResult.Ok(true));
        }

        private async Task GetInsights()
        {
            if(!string.IsNullOrEmpty(recordModel.Description))
            {
                CaseDto _case = new()
                {
                    message = recordModel.Description
                };
                //Pass the description from the health record to the Ai Service
                if (!string.IsNullOrEmpty(_case.message))
                {
                    _isInsightReady = false;
                    _isPreparingInsights = true;

                    patientSuggestions = (await AiService.GetSuggestionsForPatientAsync(_case)).Value.ToList();
                    relatedCases = (await AiService.GetRelatedMedicalCasesAsync(_case)).Value.ToList();
                    suggestedTests = (await AiService.GetSuggestedMedicalTestsAsync(_case)).Value.ToList();
                    caseInsight = (await AiService.GetCaseInsightsAsync(_case)).Value.ToList();

                    _isInsightReady = true;
                    _isPreparingInsights = false;
                }
            }
        }
        void Cancel() => MudDialog.Cancel();
    }
}

