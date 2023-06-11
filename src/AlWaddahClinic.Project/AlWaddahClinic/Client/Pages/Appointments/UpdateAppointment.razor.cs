using System;
using AlWaddahClinic.Client.Pages.Patients;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Appointments
{
	public partial class UpdateAppointment : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IPatientsService PatientsService { get; set; } = default!;

        [Inject]
		public IAppointmentsService AppointmentsService { get; set; } = default!;

		[Parameter] public Guid AppointmentId { get; set; }
        [Parameter] public string PatientName { get; set; } = default!;

        private AppointmentDto oldAppointment = new();
        private AppointmentUpdateDto model = new();

        private string _errorMessage = string.Empty;    
        private bool _isMakingRequest = false;
        private bool _isBusy = true;
        private TimeSpan? time = new TimeSpan();
        private DateTime? appointmentDay = DateTime.Now;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                oldAppointment = (await AppointmentsService.GetAppointmentByIdAsync(AppointmentId)).Value;
                _isBusy = false;
            }
            catch(NotFoundException ex)
            {
                _errorMessage = ex.Message;
            }
        }

        private async Task UpdateAsync()
        {
            _errorMessage = string.Empty;
            _isMakingRequest = true;
            try
            {
                model.StartAt = new DateTime(appointmentDay.Value.Year, appointmentDay.Value.Month, appointmentDay.Value.Day, time.Value.Hours, time.Value.Minutes, 0);
                await AppointmentsService.UpdateAppointmentAsync(AppointmentId ,model);
                NavigationManager.NavigateTo("/appointments");
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo("/appointments");
        }
    }
}

