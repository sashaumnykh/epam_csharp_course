using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class InvalidReportFormatException : Exception
    {
        public Object _report { get; }

        public InvalidReportFormatException() { }

        public InvalidReportFormatException(string message) : base(message) { }

        public InvalidReportFormatException(string message, Exception inner) : base(message, inner) { }

        public InvalidReportFormatException(string message, Object report) : this(message)
        {
            _report = report;
        }
    }
}
