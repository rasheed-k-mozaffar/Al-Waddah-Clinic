using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
	public partial class RecordsHistory : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Inject]
		public IHealthRecordsService HealthRecordsService { get; set; } = default!;

		[Inject]
		public IDialogsHandler DialogsHandler { get; set; } = default!;

		//Matching the route parameter
		[Parameter] public Guid PatientId { get; set; }
		[Parameter] public string PatientName { get; set; }

		private bool _isBusy = true;
		private string _errorMessage = string.Empty;

		private ApiResponse<IEnumerable<HealthRecordSummaryDto>> result = new();
		private List<HealthRecordSummaryDto>? records = new();

        protected override async Task OnInitializedAsync()
        {
			_errorMessage = string.Empty;
			try
			{
                result = await HealthRecordsService.GetRecordsForPatientAsync(PatientId);

				//If the server responded with a success status code, then store the records in a collection.
				if(result.IsSuccess)
				{
					records = result.Value.ToList();
				}
            }
			catch(DomainException ex)
			{
				_errorMessage = ex.Message;
			}
			_isBusy = false;
        }

		private async Task OpenRemoveDialog(Guid recordId)
		{

		}
    }
}

