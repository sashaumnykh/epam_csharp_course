using Common.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Validator
    {
        public static bool IsValid(int id)
        {
            if (id < 1)
            {
                return false;
            }
            return true;
        }
    }
}
