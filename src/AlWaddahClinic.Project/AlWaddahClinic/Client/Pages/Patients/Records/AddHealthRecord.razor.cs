using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
	public partial class AddHealthRecord : ComponentBase
	{
		#region Injected Services
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Inject]
		public IPatientsService PatientsService { get; set; } = default!;

		[Inject]
		public IHealthRecordsService HealthRecordsService { get; set; } = default!;

		[Inject]
		public IAiService AiService { get; set; } = default!;
		#endregion

		[Parameter] public Guid PatientId { get; set; }

		private ApiResponse<PatientDto> result = new();
		private PatientDto patient = new();
		private HealthRecordCreateDto model = new();
		private List<NoteCreateDto>? notes = new();

		#region Smart Insights Collections
		private List<string>? patientSuggestions = new();
		private List<string>? relatedCases = new();
		private List<string>? suggestedTests = new();
		private List<string>? caseInsight = new();
		#endregion

		private string _errorMessage = string.Empty;
		private string _noteTitle = string.Empty;
		private string _insightsButtonTitle => _isPreparingInsights ? "Lodaing..." : "Generate Insights";
		private string InsightsPannelText => _isPreparingInsights ? "The insights should be generated in a few seconds, please wait..." : "Get Ai Generated Insights Regarding Your Patient's Case! Just Click The Lightbulb";

		private bool _isInsightReady = false;
		private bool _isPreparingInsights = false;

		protected override async Task OnInitializedAsync()
		{
			result = await PatientsService.GetPatientById(PatientId);
			patient = result.Value;
		}

		private async Task AddRecord()
		{
			_errorMessage = string.Empty;

			if (string.IsNullOrWhiteSpace(model.Description))
			{
				_errorMessage = "Please fill in the required details for the health record";
				return;
			}
			_errorMessage = string.Empty;
			try
			{
				model.Notes = notes;
				//Mapping the Ai insights.
				model.PatientSuggestion = patientSuggestions;
				model.RelatedMedicalCases = relatedCases;
				model.MedicalCaseInsight = caseInsight;
				model.SuggestedMedicalTests = suggestedTests;
				await HealthRecordsService.AddRecordAsync(PatientId, model);
				NavigationManager.NavigateTo($"/patients/{PatientId}");
			}
			catch (DomainException ex)
			{
				_errorMessage = ex.Message;
			}
		}

		private void AddNote()
		{
			if (!string.IsNullOrEmpty(_noteTitle))
			{
				notes.Add(new NoteCreateDto { Title = _noteTitle });
			}

			_noteTitle = string.Empty;
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

				patientSuggestions = (await AiService.GetSuggestionsForPatientAsync(_case)).Value.ToList();
				relatedCases = (await AiService.GetRelatedMedicalCasesAsync(_case)).Value.ToList();
				suggestedTests = (await AiService.GetSuggestedMedicalTestsAsync(_case)).Value.ToList();
				caseInsight = (await AiService.GetCaseInsightsAsync(_case)).Value.ToList();

				_isInsightReady = true;
				_isPreparingInsights = false;
			}
		}

		private void GoBack()
		{
			NavigationManager.NavigateTo($"/patients/{PatientId}");
		}
	}
}

