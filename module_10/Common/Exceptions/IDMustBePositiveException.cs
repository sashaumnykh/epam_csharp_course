using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    [Serializable]
    public class IDMustBePositiveException : Exception
    {
        public int ID { get; }
        public IDMustBePositiveException() { }

        public IDMustBePositiveException(string message) : base(message) { }

        public IDMustBePositiveException(string message, Exception inner) : base(message, inner) { }

        public IDMustBePositiveException(string message, int id)
        : this(message)
        {
            ID = id;
        }
    }
}
