using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookLibrary.Exceptions
{
    public class NoDataFoundException : Exception
    {
        public NoDataFoundException(string message):base(message) { }
    }
}
