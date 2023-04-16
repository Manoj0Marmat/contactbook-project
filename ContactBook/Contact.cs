using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook
{
    public class Contact
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public long PersonNumber { get; set; }
        public string PersonEmail { get; set; }

        public Contact(string personName, long personNumber, string personEmail)
        {
            this.PersonName = personName;
            this.PersonNumber = personNumber;
            this.PersonEmail = personEmail;
        }

        public override string ToString()
        {
            return " Name:" + this.PersonName + ", PhNum:" + this.PersonNumber + ", Email:" + PersonEmail;
        }
    }
}
