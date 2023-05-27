using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Patients
{
	public partial class Patient : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = null!;

		[Inject]
		public IPatientsService PatientsService { get; set; } = null!;

		[Inject]
		public IDialogService DialogService { get; set; } = null!;

		//Route parameter
		[Parameter]
		public int Id { get; set; }


		ApiResponse<PatientDto> result = new();
		PatientDto patient = new();

		private string _recordsTableHeader = string.Empty;
		private string _errorMessage = string.Empty;
		private bool _isBusy = true;

        protected override async Task OnInitializedAsync()
        {
			_errorMessage = string.Empty;
			try
			{
				result = await PatientsService.GetPatientById(Id);

				if(result.IsSuccess)
				{
					patient = result.Value;
					_isBusy = false;
					_recordsTableHeader = $"Health Records ({patient.HealthRecords.Count})";
                }
			}
			catch(NotFoundException ex)
			{
				_errorMessage = ex.Message;
			}
        }

		private void GoToRecord(int recordId)
		{
			NavigationManager.NavigateTo($"/patients/records/{recordId}");
		}
		
		private async void OpenRemoveDialog()
		{
			var parameters = new DialogParameters();
			parameters.Add("Header", "Confirm Removal");
			parameters.Add("Content", "Do you really want to remove this patient? Please note that once the process has completed, you cannot invert it");
			parameters.Add("ButtonText", "Remove");
			parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

			var dialog = DialogService.Show<Dialog>("Delete", parameters, options);
			var result = await dialog.Result;

			if(!result.Canceled)
			{
				await RemovePatientAsync();
			}
        }

		private void GoToAddHealthRecord()
		{
			NavigationManager.NavigateTo($"/patients/records/add/{patient.Id}");
		}

		private void GoToUpdateRecord(int recordId)
		{
			NavigationManager.NavigateTo($"/patients/records/update/{recordId}");
		}

		private async Task RemovePatientAsync()
		{
			await PatientsService.RemovePatient(Id);
			NavigationManager.NavigateTo("/patients/all");
		}
    }
}

