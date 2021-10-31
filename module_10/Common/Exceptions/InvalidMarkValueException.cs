using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class InvalidMarkValueException : Exception
    {
        int _mark { get; }

        public InvalidMarkValueException() { }

        public InvalidMarkValueException(string message) : base(message) { }

        public InvalidMarkValueException(string message, Exception inner) : base(message, inner) { }

        public InvalidMarkValueException(string message, int mark) : this(message)
        {
            _mark = mark;
        }
    }
}
