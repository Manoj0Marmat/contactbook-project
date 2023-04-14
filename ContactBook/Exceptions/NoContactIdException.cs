using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBookLibrary.Exceptions
{
    public class NoContactIdException:Exception 
    {
        public NoContactIdException(string message):base(message) { }
    }
}
