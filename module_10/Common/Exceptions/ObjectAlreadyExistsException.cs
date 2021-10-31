using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public Object _object { get; }

        public ObjectAlreadyExistsException() { }

        public ObjectAlreadyExistsException(string message) : base(message) { }

        public ObjectAlreadyExistsException(string message, Exception inner) : base(message, inner) { }

        public ObjectAlreadyExistsException(string message, Object obj) : this(message)
        {
            _object = obj;
        }
    }
}
