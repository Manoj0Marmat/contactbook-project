using System.Text.RegularExpressions;

namespace ContactBookLibrary
{
    public class ContactValidation
    {
        public bool personName_Validation(string name)
        {
            // Define a regular expression pattern for validating the name
            string pattern = @"^[A-Za-z\s.'-]{2,50}$";

            // Create a regular expression object and use it to match the name
            Regex regex = new Regex(pattern);
            bool isValid = regex.IsMatch(name);

            if (isValid)
            {
                return true;
            }

            return false;
        }

        public bool personNumber_Validation(int numberLen)
        {
            if (numberLen != 10)
            {
                return false;
            }

            return true;
        }

        public bool personEmail_Validation(string email)
        {
            // Define a regular expression pattern for validating email addresses
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Create a regular expression object and use it to match the email address
            Regex regex = new Regex(pattern);
            bool isValid = regex.IsMatch(email);

            if (isValid)
            {
                return true;
            }
            return false;
        }
    }
}
