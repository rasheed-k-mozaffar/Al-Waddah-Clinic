using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Appointments
{
    public partial class MakeNewAppointment : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = default!;

        [Inject]
        public IAppointmentsService AppointmentsService { get; set; } = default!;
        [Parameter] public Guid PatientId { get; set; }

        private ApiResponse<PatientDto> result = new();
        private AppointmentCreateDto model = new();

        private string _errorMessage = string.Empty;
        private bool _isMakingRequest = false;
        private bool _isBusy = true;
        private TimeSpan? time = new TimeSpan();
        private DateTime? appointmentDay = DateTime.Now;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                result = await PatientsService.GetPatientById(PatientId);
                _isBusy = false;
            }
            catch (NotFoundException ex)
            {
                _errorMessage = ex.Message;
            }
        }

        private async Task MakeAsync()
        {
            _errorMessage = string.Empty;
            _isMakingRequest = true;
            try
            {
                model.StartAt = new DateTime(appointmentDay.Value.Year, appointmentDay.Value.Month, appointmentDay.Value.Day, time.Value.Hours, time.Value.Minutes, 0);
                await AppointmentsService.AddAppointmentAsync(PatientId, model);
                NavigationManager.NavigateTo($"/patients/{PatientId}");

            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo($"/patients/{PatientId}");
        }
    }
}

