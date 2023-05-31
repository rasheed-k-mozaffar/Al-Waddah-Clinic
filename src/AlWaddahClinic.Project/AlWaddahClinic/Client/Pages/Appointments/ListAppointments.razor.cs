using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Appointments
{
	public partial class ListAppointments : ComponentBase
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
			catch(DomainException ex)
			{
				_errorMessage = ex.Message;
			}
        }

		private async Task OpenRemoveDialog(int id)
		{
			var parameters = new DialogParameters();
			parameters.Add("Header", "Confirm Removal");
			parameters.Add("Content", "Do you really want to remove this appointment? Please note that once the process has completed it cannot be inverted");
			parameters.Add("ButtonText", "Remove");
			parameters.Add("Color", Color.Error);

			var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

			var dialog = DialogService.Show<Dialog>("Delete", parameters, options);
			var result = await dialog.Result;

			if(!result.Canceled)
			{
				await RemoveAsync(id);
			}
		}

		private void GoToUpdateAppointment(string patientName, int appointmentId)
		{
			NavigationManager.NavigateTo($"/appointments/reschedule/{patientName}/{appointmentId}");
		}

		private void GoToPastAppointments()
		{
			NavigationManager.NavigateTo("/appointments/past");
		}

		private async Task RemoveAsync(int id)
		{
			try
			{
				await AppointmentsService.RemoveAppointmentAsync(id);
				var itemToRemove = appointments.SingleOrDefault(a => a.Id == id);
				appointments.Remove(itemToRemove);
			}
			catch(DomainException ex)
			{
				_errorMessage = ex.Message;
			}
		}
    }
}

