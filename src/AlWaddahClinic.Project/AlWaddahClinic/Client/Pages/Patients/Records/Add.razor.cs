using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
	public partial class Add : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = null!;

		[Inject]
		public IPatientsService PatientsService { get; set; } = null!;

		[Parameter] public int PatientId { get; set; }

		private ApiResponse<PatientDto> result = new();
		private PatientDto patient = new();
		private HealthRecordDto model = new();

		private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
			result = await PatientsService.GetPatientById(PatientId);
			patient = result.Value;
        }

		//TODO: Implement this method
        private async Task AddRecord()
		{
			throw new NotImplementedException();
		}
	}
}

