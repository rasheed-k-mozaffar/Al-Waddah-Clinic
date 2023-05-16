using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
	public partial class Patient : ComponentBase
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; } = null!;

		[Inject]
		public IPatientsService PatientsService { get; set; } = null!;

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
    }
}

