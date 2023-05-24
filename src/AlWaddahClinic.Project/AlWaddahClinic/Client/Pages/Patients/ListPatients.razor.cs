using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
    public partial class ListPatients : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = null!;

        ApiResponse<IEnumerable<PatientSummaryDto>> patients = new();


        private void GoToAddPatient()
        {
            NavigationManager.NavigateTo("/patients/add");
        }

        protected override async Task OnInitializedAsync()
        {
            patients = await PatientsService.GetAllPatients();
        }
    }
}

