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

		private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
			_errorMessage = string.Empty;
			try
			{
				result = await PatientsService.GetPatientById(Id);

				if(result.IsSuccess)
				{
					patient = result.Value;
                }
			}
			catch(NotFoundException ex)
			{
				_errorMessage = ex.Message;
			}
        }

		//TODO: Implement this method
		private void GoToRecord(int id)
		{
			throw new NotImplementedException();
		}
		//TODO: Create the open dialog functionality
		private void OpenRemoveDialog()
		{
			var parameters = new DialogParameters();
			parameters.Add("Header", "Confirm Removal");
			parameters.Add("Content", "Do you really want to remove this patient? Please note that once the process has completed, you cannot invert it");
			parameters.Add("ButtonText", "Remove");
			parameters.Add("OkClicked", RemovePatientAsync());

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

			DialogService.Show<Dialog>("Delete", parameters, options);
        }

		private void GoToAddHealthRecord()
		{
			NavigationManager.NavigateTo($"/patients/records/add/{patient.Id}");
		}

		private async Task RemovePatientAsync()
		{
			await PatientsService.RemovePatient(Id);
			NavigationManager.NavigateTo("/patients/all");
		}
    }
}

