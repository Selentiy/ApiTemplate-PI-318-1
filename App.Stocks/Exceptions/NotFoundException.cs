using System;

namespace App.Stocks.Exceptions
{
	public class NotFoundException : Exception
	{
		public int NotFoundId { get; private set; }
		public Type NotFoundType { get; private set; }

		public NotFoundException(Type type, int id) :
			base($"The object with {type} type and {id} id is not found!")
		{
			NotFoundId = id;
			NotFoundType = type;
		}
	}
}
