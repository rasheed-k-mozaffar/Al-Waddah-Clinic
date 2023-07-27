using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Patients
{
    public partial class ListPatients : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        [Inject]
        public IDialogsHandler DialogsHandler { get; set; } = default!;

        ApiResponse<IEnumerable<PatientSummaryDto>> result = new();
        List<PatientSummaryDto> patients = new();
        List<PatientSummaryDto> filteredPatients = new();

        private string _searchText = string.Empty;
        private void GoToAddPatient()
        {
            NavigationManager.NavigateTo("/patients/add");
        }

        protected override async Task OnInitializedAsync()
        {
            result = await PatientsService.GetAllPatients();
            patients = result.Value.ToList();
        }

        public async Task HandlePatientRemoval()
        {
            result = await PatientsService.GetAllPatients();
            patients = result.Value.ToList();
        }

        private void OpenSearchDialog()
        {
            var options = new DialogOptions() { ClassBackground = "dialog-background-blur", MaxWidth = MaxWidth.Medium, Position = DialogPosition.TopCenter };
            var dialog = DialogService.Show<PatientSearchDialog>("Search", options);
        }

        private void Search()
        {
            filteredPatients = patients.Where(p => p.FullName.ToLower().Contains(_searchText.ToLower())).ToList();
            _searchText = string.Empty;
        }

        private void GoToSearchResults()
        {
            if (!string.IsNullOrEmpty(_searchText))
            {
                NavigationManager.NavigateTo($"/patients/search/{_searchText}");
            }
        }
    }
}

