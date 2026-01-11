using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicardSync
{
    [Serializable()]
    public  class UpdateConcurrencyException : Exception
    {
        public UpdateConcurrencyException()
        {
        }

        public UpdateConcurrencyException(string message)
            : base(message)
        {
        }

        public UpdateConcurrencyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UpdateConcurrencyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
