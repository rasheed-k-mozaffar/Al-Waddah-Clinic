using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using AlWaddahClinic.Shared.Enums;
using AKSoftware.Localization.MultiLanguages;

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

        [Inject]
        public ILanguageContainerService Lang { get; set; } = default!; 

        private List<AppointmentSummaryDto>? result;
        private List<AppointmentSummaryDto>? appointments;
        private List<AppointmentSummaryDto>? upcoming;
        private List<AppointmentSummaryDto>? completed;
        private List<AppointmentSummaryDto>? missed;
        private List<AppointmentSummaryDto>? unnoted;

        private bool _isBusy = true;
        private string _errorMessage = string.Empty;
        private int _actionsRequired = 0;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                result = (await AppointmentsService.GetAllAppointmentsAsync()).Value.ToList();
                appointments = result;

                upcoming = result.Where(a => a.StartAt > DateTime.Now.AddMinutes(-1)).ToList();
                completed = result.Where(a => a.Status == AppointmentStatusEnum.Completed).ToList();
                missed = result.Where(a => a.Status == AppointmentStatusEnum.Missed).ToList();
                unnoted = result.Where(a => a.StartAt < DateTime.Now.AddMinutes(-1) && a.Status == null).ToList();
                _isBusy = false;
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }
        }

        private async Task OpenRemoveDialog(Guid id)
        { 
            var parameters = new DialogParameters();
            parameters.Add("Header", Lang.Keys["Dialogs:ConfirmRemoval"]);
            parameters.Add("Content", Lang.Keys["Dialogs:RemovalMessage"]);
            parameters.Add("ButtonText", Lang.Keys["Dialogs:Confirm"]);
            parameters.Add("CancelButton", Lang.Keys["Dialogs:Cancel"]);
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = DialogService.Show<Dialog>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await RemoveAsync(id);
            }
        }

        private async Task OpenActionDialog(Guid patientId, Guid appointmentId)
        {
            var parameters = new DialogParameters();
            parameters.Add("PatientId", patientId);
            parameters.Add("AppointmentId", appointmentId);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Large };

            var dialog = DialogService.Show<AppointmentStatusDialog>("Appointment Check",parameters, options);
            var result = dialog.Result;

            if(!result.IsCanceled)
            {
                StateHasChanged();
            }
        }

        private void GoToUpdateAppointment(string patientName, Guid appointmentId)
        {
            NavigationManager.NavigateTo($"/appointments/reschedule/{patientName}/{appointmentId}");
        }


        private async Task RemoveAsync(Guid id)
        {
            try
            {
                await AppointmentsService.RemoveAppointmentAsync(id);
                var itemToRemove = result.SingleOrDefault(a => a.Id == id);

                if(itemToRemove.Status == null) {
                    upcoming.Remove(itemToRemove);
                }
                else if(itemToRemove.Status == AppointmentStatusEnum.Missed) {
                    missed.Remove(itemToRemove);
                }
                else if(itemToRemove.Status == AppointmentStatusEnum.Completed) {
                    completed.Remove(itemToRemove);
                }
                else {
                    unnoted.Remove(itemToRemove);
                }
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }
        }
    }
}

