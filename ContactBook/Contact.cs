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

        public Contact(string name, long number, string email)
        {
            this.PersonName = name;
            this.PersonNumber = number;
            this.PersonEmail = email;
        }

        public override string ToString()
        {
            return " Name:" + this.PersonName + ", PhNum:" + this.PersonNumber + ", Email:" + PersonEmail;
        }
    }
}
