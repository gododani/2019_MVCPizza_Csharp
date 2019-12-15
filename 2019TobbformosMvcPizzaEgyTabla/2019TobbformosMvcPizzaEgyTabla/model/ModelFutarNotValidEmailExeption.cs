using System;
using System.Runtime.Serialization;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    [Serializable]
    internal class ModelFutarNotValidEmailExeption : Exception
    {
        public ModelFutarNotValidEmailExeption()
        {
        }

        public ModelFutarNotValidEmailExeption(string message) : base(message)
        {
        }

        public ModelFutarNotValidEmailExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelFutarNotValidEmailExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}