using System;
namespace AlWaddahClinic.Server.Exceptions
{
	public class UndefinedCaseException : Exception
	{
		public UndefinedCaseException(string message) : base(message)
		{
		}
	}
}

