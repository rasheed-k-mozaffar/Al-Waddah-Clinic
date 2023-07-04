using System;
using MudBlazor;

namespace AlWaddahClinic.Client.UiServices
{
	public interface IDialogsHandler
	{
        #region Deletion of a given resource
        Task<bool> ConfirmPatientRemovalAsync(Guid resourceId = default);
		Task ConfirmHealthRecordRemovalAsync(Guid resourceId = default);
        Task ConfirmAppointmentRemovalAsync(Guid resourceId = default);
        #endregion
        Task MakeAppointmentAsync(string header, Guid patientId = default);
		Task ConfirmLogOutAsync(string header, string content, string buttonText);

		//TODO: Add more signatures if necessary.
		//TODO: Add a signature for the search dialog
	}
}

