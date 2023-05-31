using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Appointments
{
	public partial class ListPastAppointments : ComponentBase
	{
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IAppointmentsService AppointmentsService { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private List<AppointmentSummaryDto> appointments;

        private bool _isBusy = true;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                appointments = (await AppointmentsService.GetAllAppointmentsAsync()).Value.ToList();
                _isBusy = false;
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }
        }
        private void GoToUpcomingAppointments()
        {
            NavigationManager.NavigateTo("/appointments");
        }

        private async Task RemoveAsync(int id)
        {
            try
            {
                await AppointmentsService.RemoveAppointmentAsync(id);
                var itemToRemove = appointments.SingleOrDefault(a => a.Id == id);
                appointments.Remove(itemToRemove);
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }
        }
    }
}

