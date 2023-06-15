using System;
namespace AlWaddahClinic.Server.Extensions
{
	public class ClinicRegisterationFailedException : Exception
	{
		public ClinicRegisterationFailedException(string message) : base(message)
		{
		}
	}
}

