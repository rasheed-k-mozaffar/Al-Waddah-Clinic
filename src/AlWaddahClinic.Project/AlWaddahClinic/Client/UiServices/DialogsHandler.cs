using System;
using AKSoftware.Localization.MultiLanguages;
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
        private readonly ILanguageContainerService _lang;

        public DialogsHandler(NavigationManager navigationManager, IDialogService dialogService, IPatientsService patientsService, IHealthRecordsService healthRecordsService, IAppointmentsService appointmentsService, ILanguageContainerService lang)
        {
            //Assigning the received objects from DIC to the private fields
            _navigationManager = navigationManager;
            _dialogService = dialogService;
            _patientsService = patientsService;
            _healthRecordsService = healthRecordsService;
            _appointmentsService = appointmentsService;
            _lang = lang;
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
            parameters.Add("Header", _lang.Keys["Dialogs:ConfirmRemoval"]);
            parameters.Add("Content", _lang.Keys["Dialogs:RemovalMessage"]);
            parameters.Add("ButtonText", _lang.Keys["Dialogs:Confirm"]);
            parameters.Add("CancelButton", _lang.Keys["Dialogs:Cancel"]);
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

