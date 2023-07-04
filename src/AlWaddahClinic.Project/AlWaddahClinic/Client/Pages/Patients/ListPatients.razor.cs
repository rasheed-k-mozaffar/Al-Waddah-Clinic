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

        ApiResponse<IEnumerable<PatientSummaryDto>> patients = new();

        private void GoToAddPatient()
        {
            NavigationManager.NavigateTo("/patients/add");
        }

        protected override async Task OnInitializedAsync()
        {
            patients = await PatientsService.GetAllPatients();
        }

        public async Task HandlePatientRemoval()
        {
            patients = await PatientsService.GetAllPatients();
        }

        private void OpenSearchDialog()
        {
            var options = new DialogOptions() { ClassBackground = "dialog-background-blur", MaxWidth = MaxWidth.Medium, Position = DialogPosition.TopCenter };
            var dialog = DialogService.Show<PatientSearchDialog>("Search", options);
        }
    }
}

