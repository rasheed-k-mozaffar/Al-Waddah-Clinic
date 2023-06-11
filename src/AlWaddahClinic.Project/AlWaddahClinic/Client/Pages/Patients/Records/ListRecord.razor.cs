using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Patients.Records
{
	public partial class ListRecord : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Inject]
		public IHealthRecordsService HealthRecordsService { get; set; } = default!;

		[Inject]
		public IDialogService DialogService { get; set; } = default!;

		//Matching the route params
		[Parameter] public Guid Id { get; set; }

		private ApiResponse<HealthRecordDto> result = new();
		private HealthRecordDto record = new();

		private string _patientName = string.Empty;
		private string _errorMessage = string.Empty;
		private bool _isBusy = true;

        protected override async Task OnInitializedAsync()
        {
			_errorMessage = string.Empty;
			try
			{
                result = await HealthRecordsService.GetHealthRecordByIdAsync(Id);

				if(result.IsSuccess)
				{
					record = result.Value;
				}

				_isBusy = false;
            }
			catch(DomainException ex)
			{
				//TODO: Handle the error
				_errorMessage = ex.Message;
			}
        }

		private async Task OpenRemoveDialog()
		{
			var parameters = new DialogParameters();
			parameters.Add("Header" ,"Confirm Removal");
			parameters.Add("Content", "Do you really want to remove this health record? By confirming the process, all related notes will be removed as well");
			parameters.Add("ButtonText", "Remove");
			parameters.Add("Color", Color.Error);

			var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

			var dialog = DialogService.Show<Dialog>("Delete", parameters, options);
			var result = await dialog.Result;

			if(!result.Canceled)
			{
				await RemoveRecordAsync();
			}
		}

		private async Task RemoveRecordAsync()
		{
			try
			{
				await HealthRecordsService.RemoveRecordAsync(Id);
				NavigationManager.NavigateTo($"/patients/{record.Patient.Id}");
			}
			catch(DomainException ex)
			{
				_errorMessage = ex.Message;
			}
		}

		private void GoBack()
		{
			NavigationManager.NavigateTo($"/patients/{record.Patient.Id}");
		}
    }
}

