	using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Components
{
	public partial class PatientSearchDialog : ComponentBase
	{
		[Inject]
		public IPatientsService PatientsService { get; set; } = default!;

		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Parameter] public List<PatientSummaryDto>? PatientsToSearch { get; set; }

        ApiResponse<IEnumerable<PatientSummaryDto>> searchResults = new();

        private string _searchText = string.Empty;
		private bool _isBusy = false;

		private void GoToSearchResults(string searchQuery)
		{
			NavigationManager.NavigateTo($"/patients/search/{searchQuery}");
		}

        private void Cancel() => MudDialog.Cancel();

        private void GoToPatient(Guid patientId)
		{
			NavigationManager.NavigateTo($"/patients/{patientId}");
		}
	}
}

