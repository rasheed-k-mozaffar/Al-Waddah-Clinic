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

		ApiResponse<HealthRecordDto> record = new();
		private HealthRecordUpdateDto model = new();

		private string _errorMessage = string.Empty;
		private bool _isMakingRequest = false;
		private bool _isBusy = true;

        protected override async Task OnInitializedAsync()
        {
			try
			{
				
			}
			catch(DomainException ex)
			{
				_errorMessage = ex.Message;
			}
        }

        private HealthRecordUpdateDto MapDetails(HealthRecordDto recordDto)
		{
			return new HealthRecordUpdateDto
			{
				Description = recordDto.Description,
				Notes = recordDto.Notes.Select(n => new NoteUpdateDto { Title = n.Title}).ToList()
			};
		}
	}
}

