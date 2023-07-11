using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Components
{
    public partial class AppointmentStatusDialog : ComponentBase
    {
        [Inject]
        public IAppointmentsService AppointmentsService { get; set; } = default!;

        [Parameter] public Guid PatientId { get; set; }
        [Parameter] public Guid AppointmentId { get; set; }

        private List<NoteCreateDto>? notes = new();
        private HealthRecordCreateDto? recordModel = new();
        private AppointmentStatusCheckDto model = new();

        private string _noteTitle = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isMakingRequest = false;

        private void AddNote()
        {
            if (!string.IsNullOrEmpty(_noteTitle))
            {
                notes.Add(new NoteCreateDto { Title = _noteTitle });
            }

            _noteTitle = string.Empty;
        }


        private async Task Submit()
        {
            _isMakingRequest = true;
            _errorMessage = string.Empty;
            try
            {
                model.HealthRecordCreate = recordModel;
                await AppointmentsService.CompleteAppointmentAsync(AppointmentId, model);
            }
            catch(DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
            MudDialog.Close(DialogResult.Ok(true));
        }
        void Cancel() => MudDialog.Cancel();
    }
}

