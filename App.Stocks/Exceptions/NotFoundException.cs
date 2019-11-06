using System;

namespace App.Stocks.Exceptions
{
	public class NotFoundException : Exception
	{
		public int NotFoundId { get; private set; }
		public Type NotFoundType { get; private set; }
		public new string Message => $"The object with {NotFoundType} type and {NotFoundId} id is not found!";

		public NotFoundException(Type type, int id)
		{
			NotFoundId = id;
			NotFoundType = type;
		}
	}
}
