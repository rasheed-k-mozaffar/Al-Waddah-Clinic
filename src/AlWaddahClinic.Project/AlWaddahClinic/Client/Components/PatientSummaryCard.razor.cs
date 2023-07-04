using System;
using AlWaddahClinic.Client.Pages.Patients;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlWaddahClinic.Client.Components
{
	public partial class PatientSummaryCard : ComponentBase
	{
		[Inject]
		public IDialogsHandler DialogsHandler { get; set; } = default!;

		[Inject]
		public NavigationManager NavigationManager { get; set; } = default!;

		[Parameter] public PatientSummaryDto Patient { get; set; } = null!;

		[Parameter] public EventCallback OnPatientRemoval { get; set; }

        private async Task OpenRemoveDialog(Guid patientId)
		{
			var result = await DialogsHandler.ConfirmPatientRemovalAsync(patientId);

			//The user confirmed the removal.
			if(result)
			{
				await OnPatientRemoval.InvokeAsync();
			}
		}

		private async Task OpenMakeAppointmentDialog(Guid patientId, string patientName)
		{
			await DialogsHandler.MakeAppointmentAsync($"Make Appointment for {patientName}", patientId);
		}
 	}
}

