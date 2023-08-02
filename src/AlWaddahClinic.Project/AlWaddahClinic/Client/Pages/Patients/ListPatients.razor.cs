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
    }
}

