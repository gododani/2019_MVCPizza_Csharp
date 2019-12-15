using System;
using System.Runtime.Serialization;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    [Serializable]
    internal class ModelFutarNotValidLakcimExeption : Exception
    {
        public ModelFutarNotValidLakcimExeption()
        {
        }

        public ModelFutarNotValidLakcimExeption(string message) : base(message)
        {
        }

        public ModelFutarNotValidLakcimExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelFutarNotValidLakcimExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}