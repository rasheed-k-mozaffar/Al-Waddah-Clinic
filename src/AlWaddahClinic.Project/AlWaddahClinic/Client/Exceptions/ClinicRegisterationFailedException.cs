using System;
namespace AlWaddahClinic.Client.Exceptions
{
	public class ClinicRegisterationFailedException : Exception
	{
		public ClinicRegisterationFailedException(string message) : base(message)
		{
		}
	}
}

