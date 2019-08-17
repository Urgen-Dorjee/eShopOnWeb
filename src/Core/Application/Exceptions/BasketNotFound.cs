using System;
using System.Runtime.Serialization;

namespace eWebShop.Application.Exceptions
{
    public class BasketNotFound : Exception
    {
        public BasketNotFound(int basketId) : base($"Could not find the Basket with ID{basketId}")
        { }

        public BasketNotFound(string message) : base(message)
        {
        }
        public BasketNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected BasketNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
