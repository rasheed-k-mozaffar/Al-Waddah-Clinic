using System;
using Microsoft.AspNetCore.Components;

namespace AlWaddahClinic.Client.Pages.Patients
{
	public partial class UpdatePatient : ComponentBase
	{
		//TODO: inject the required objects

		[Parameter] public int PatientId { get; set; }

        //TODO: implement the methods
        protected override async Task OnInitializedAsync()
        {
			throw new NotImplementedException();
        }

        private async Task Update()
		{
			throw new NotImplementedException();
		}
	}
}

