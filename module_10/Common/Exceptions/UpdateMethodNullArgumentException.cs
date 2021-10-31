using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class UpdateMethodNullArgumentException : Exception
    {
        public Object _toUpdateObject { get; }

        public UpdateMethodNullArgumentException() { }

        public UpdateMethodNullArgumentException(string message) : base(message) { }

        public UpdateMethodNullArgumentException(string message, Exception inner) : base(message, inner) { }

        public UpdateMethodNullArgumentException(string message, Object toUpdateObject) : this(message)
        {
            _toUpdateObject = toUpdateObject;
        }
    }
}
