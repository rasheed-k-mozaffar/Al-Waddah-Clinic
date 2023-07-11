using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Components
{
    public partial class UpcomingAppointments : ComponentBase
    {
        private bool _isDense = false;
        private bool _isBusy = true;
        private bool _isFetchingInformation = false;

        private string _errorMessage = string.Empty;

        #region Injected Services
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IDialogsHandler DialogsHandler { get; set; } = default!;

        [Inject]
        public IAppointmentsService AppointmentsService { get; set; } = default!;
        #endregion
        private ApiResponse<IEnumerable<AppointmentSummaryDto>> result = new();
        private List<AppointmentSummaryDto> appointments = new();


        protected override async Task OnInitializedAsync()
        {
            _errorMessage = string.Empty;
            _isFetchingInformation = true;
            try
            {
                result = await AppointmentsService.GetAllAppointmentsAsync();

            if (result.IsSuccess)
            {
                appointments = result.Value.Where(a => a.StartAt.Value < DateTime.Now.AddHours(24) && a.StartAt.Value > DateTime.Now).ToList();

            }
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isBusy = false;
            _isFetchingInformation = false;
        }
    }
}

