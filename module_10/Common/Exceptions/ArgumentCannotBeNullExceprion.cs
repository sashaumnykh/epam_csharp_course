using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ArgumentCannotBeNullException : Exception
    {
        public Object _argument { get; }
        public ArgumentCannotBeNullException() { }

        public ArgumentCannotBeNullException(string message) : base(message) { }

        public ArgumentCannotBeNullException(string message, Exception inner) : base(message, inner) { }

        public ArgumentCannotBeNullException(string message, Object argument) : this(message)
        {
            _argument = argument;
        }
    }
}
