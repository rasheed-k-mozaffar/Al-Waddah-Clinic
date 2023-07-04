using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.UiServices
{
    public class DialogsHandler : IDialogsHandler
	{
        #region Dependency injection region
        private readonly NavigationManager _navigationManager;
        private readonly IDialogService _dialogService;
        private readonly IPatientsService _patientsService;
        private readonly IHealthRecordsService _healthRecordsService;
        private readonly IAppointmentsService _appointmentsService;

        public DialogsHandler(NavigationManager navigationManager, IDialogService dialogService, IPatientsService patientsService, IHealthRecordsService healthRecordsService, IAppointmentsService appointmentsService)
        {
            //Assigning the received objects from DIC to the private fields
            _navigationManager = navigationManager;
            _dialogService = dialogService;
            _patientsService = patientsService;
            _healthRecordsService = healthRecordsService;
            _appointmentsService = appointmentsService;
        }
        #endregion

        public Task ConfirmAppointmentRemovalAsync(Guid resourceId = default)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmHealthRecordRemovalAsync(Guid resourceId = default)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmLogOutAsync(string header, string content, string buttonText)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ConfirmPatientRemovalAsync(Guid resourceId = default)
        {
            if(resourceId == default)
            {
                throw new NoResourceIdSuppliedException("No resource ID was supplied for the removal to happen");
            }

            var parameters = new DialogParameters();
            parameters.Add("Header", "Confirm Removal");
            parameters.Add("Content", "Do you really want to remove this patient's details from the system? Please note that once the process has completed, you cannot invert it");
            parameters.Add("ButtonText", "Remove");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = _dialogService.Show<Dialog>("Remove", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await _patientsService.RemovePatient(resourceId);
                return true; //Return true if the patient was removed succesfully.
            }

            return false;
        }

        public Task MakeAppointmentAsync(string header, Guid patientId = default)
        {
            throw new NotImplementedException();
        }
    }
}

