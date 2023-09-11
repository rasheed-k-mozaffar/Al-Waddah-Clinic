using System;
using AlWaddahClinic.Client.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlWaddahClinic.Client.Pages.Patients
{
	public partial class Patient : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Inject]
		public IPatientsService PatientsService { get; set; } = default!;

		[Inject]
		public IDialogService DialogService { get; set; } = default!;

		//[Inject]
		//public IAiService AiService { get; set; } = default!;

		//Route parameter
		[Parameter]
		public Guid Id { get; set; }


		private ApiResponse<PatientDto> result = new();
		private PatientDto patient = new();

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
					_recordsTableHeader = $"Health Records ({patient.HealthRecords.Count})";
					_isBusy = false;
                }
			}
			catch(NotFoundException ex)
			{
				_errorMessage = ex.Message;
			}
        }

		private void GoToRecord(Guid recordId)
		{
			NavigationManager.NavigateTo($"/patients/records/{recordId}");
		}

		private void GoToMakeAppointment()
		{
			NavigationManager.NavigateTo($"/appointments/make/{patient.Id}");
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

		private void GoToUpdateRecord(Guid recordId)
		{
			NavigationManager.NavigateTo($"/patients/records/update/{recordId}");
		}

		//private void GoToRecordsHistory()
		//{
		//	NavigationManager.NavigateTo($"/patients/records/history/{patient.FullName}/{Id}");
		//}

		private async Task RemovePatientAsync()
		{
			await PatientsService.RemovePatient(Id);
			NavigationManager.NavigateTo("/patients/all");
		}
    }
}

