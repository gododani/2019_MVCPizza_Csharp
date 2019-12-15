using System;
using System.Runtime.Serialization;

namespace _2019TobbformosMvcPizzaEgyTabla
{
    [Serializable]
    internal class ModelFutarNotValidTelefonszamExeption : Exception
    {
        public ModelFutarNotValidTelefonszamExeption()
        {
        }

        public ModelFutarNotValidTelefonszamExeption(string message) : base(message)
        {
        }

        public ModelFutarNotValidTelefonszamExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelFutarNotValidTelefonszamExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}