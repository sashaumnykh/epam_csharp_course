using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class LecturerIsNotCreatedException : Exception
    {
        public int _lecturerID { get; }

        public LecturerIsNotCreatedException() { }

        public LecturerIsNotCreatedException(string message) : base(message) { }

        public LecturerIsNotCreatedException(string message, Exception inner) : base(message, inner) { }

        public LecturerIsNotCreatedException(string message, int id) : this(message)
        {
            _lecturerID = id;
        }
    }
}
