using System;
namespace AlWaddahClinic.Server.Exceptions
{
	public class UserCreationFailed : Exception
	{
		public UserCreationFailed(string message) : base(message)
		{
		}
	}
}

