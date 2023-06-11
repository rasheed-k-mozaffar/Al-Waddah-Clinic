using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
    public partial class UpdateRecord : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IHealthRecordsService HealthRecordsService { get; set; } = default!;

        [Parameter] public Guid Id { get; set; }

        ApiResponse<HealthRecordDto> result = new();
        private HealthRecordUpdateDto model = new();
        private List<NoteUpdateDto>? notes = new();

        private string _noteTitle = string.Empty;

        private string _errorMessage = string.Empty;
        private bool _isMakingRequest = false;
        private bool _isBusy = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                result = await HealthRecordsService.GetHealthRecordByIdAsync(Id);
                model = MapDetails(result.Value);
                _isBusy = false;
            }
            catch (DomainException ex)
            {
                _errorMessage = ex.Message;
            }
        }

        private HealthRecordUpdateDto MapDetails(HealthRecordDto recordDto)
        {
            return new HealthRecordUpdateDto
            {
                Description = recordDto.Description,
                Notes = notes
            };
        }

        private async Task UpdateAsync()
        {
            _isMakingRequest = true;

            try
            {
                await HealthRecordsService.UpdateRecordAsync(Id ,model);
                NavigationManager.NavigateTo($"/patients/{result.Value.Patient.Id}");
            }
            catch(DomainException ex)
            {
                _errorMessage = ex.Message;
            }

            _isMakingRequest = false;
        }

        private void AddNote()
        {
            if (!string.IsNullOrEmpty(_noteTitle))
            {
                notes.Add(new NoteUpdateDto { Title = _noteTitle });
            }

            _noteTitle = string.Empty;
        }

        private void GoBack()
        {
            NavigationManager.NavigateTo($"/patients/{result.Value.Patient.Id}");
        }
    }
}

