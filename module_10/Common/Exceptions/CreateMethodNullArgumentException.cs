using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class CreateMethodNullArgumentException : Exception
    {
        public Object _toCreateObject { get; }

        public CreateMethodNullArgumentException() { }

        public CreateMethodNullArgumentException(string message) : base(message) { }

        public CreateMethodNullArgumentException(string message, Exception inner) : base(message, inner) { }

        public CreateMethodNullArgumentException(string message, Object toCreateObject) : this(message)
        {
            _toCreateObject = toCreateObject;
        }
    }
}
