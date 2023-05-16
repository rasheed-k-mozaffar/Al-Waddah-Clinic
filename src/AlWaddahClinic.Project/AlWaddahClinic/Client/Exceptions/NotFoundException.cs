using System;
namespace AlWaddahClinic.Client.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string message) : base(message)
		{
		}
	}
}

