using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class LectureIsNotCreatedException : Exception
    {
        public int _lecturerID {get; }

        public LectureIsNotCreatedException() { }

        public LectureIsNotCreatedException(string message) : base(message) { }

        public LectureIsNotCreatedException(string message, Exception inner) : base(message, inner) { }

        public LectureIsNotCreatedException(string message, int id) : this(message)
        {
            _lecturerID = id;
        }
    }
}
