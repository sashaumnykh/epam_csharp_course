using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class StudentIsNotCreatedException : Exception
    {
        public int _studentID {get; }

        public StudentIsNotCreatedException() { }

        public StudentIsNotCreatedException(string message) : base(message) { }

        public StudentIsNotCreatedException(string message, Exception inner) : base(message, inner) { }

        public StudentIsNotCreatedException(string message, int id) : this(message)
        {
            _studentID = id;
        }
    }
}
