using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
	public partial class SearchResults : ComponentBase
	{
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = null!;

        ApiResponse<IEnumerable<PatientSummaryDto>> patients = new();

        [Parameter] public string SearchText { get; set; }

        private bool _isBusy = true;
        private string _errorMessage = string.Empty;

        private void GoToAddPatient()
        {
            NavigationManager.NavigateTo("/patients/add");
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                patients = await PatientsService.SearchForPatients(SearchText);
                _isBusy = false;
            }
            catch(NotFoundException ex)
            {
                _errorMessage = ex.Message;
            }
        }
    }
}

