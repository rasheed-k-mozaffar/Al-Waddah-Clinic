using System;
namespace AlWaddahClinic.Client.Exceptions
{
	public class DataRetrievalException : Exception
	{
		public DataRetrievalException(string message) : base(message)
		{
		}
	}
}

