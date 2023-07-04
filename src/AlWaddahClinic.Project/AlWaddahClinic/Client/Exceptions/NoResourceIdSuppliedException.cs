using System;
namespace AlWaddahClinic.Client.Exceptions
{
	public class NoResourceIdSuppliedException : Exception
	{
		public NoResourceIdSuppliedException(string message) : base(message)
		{
		}
	}
}

