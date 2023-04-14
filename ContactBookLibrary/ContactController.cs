namespace ContactBookLibrary
{
    public class ContactController
    {
        public ContactValidation ContactValidation = new ContactValidation();

        // Adds a new contact to the database
        public bool Add(Contact contact)
        {
            ContactBussiness _contactBusiness = new ContactBussiness();
            return _contactBusiness.AddContactToDatabase(contact);
        }

        // Removes a contact from the database by their ID
        public bool Remove(int id)
        {
            ContactBussiness _contactBusiness = new ContactBussiness();
            return _contactBusiness.RemoveContactFromDatabase(id);
        }

        // Update a contact from the database by their ID
        public bool Update(Contact contact)
        {
            ContactBussiness _contactBusiness = new ContactBussiness();
            return _contactBusiness.UpdateContactInDatabase(contact);

        }

        // Clears all contacts from the database by truncating the table and resetting the auto-increment value
        public bool Clear()
        {
            ContactBussiness _contactBusiness = new ContactBussiness();
            return _contactBusiness.TruncateContactbookTableAndReseedPersonId();
        }

        // Retrieves all contacts from the database
        public void Get(ref List<Contact> contacts)
        {
            ContactBussiness _contactBusiness = new ContactBussiness();
            _contactBusiness.RetrieveContacts(ref contacts);
        }

        // Find a contact from the database by their ID
        public void Finds(ref List<Contact> contacts, string name)
        {
            ContactBussiness _contactBusiness = new ContactBussiness();
            _contactBusiness.FindContacts(ref contacts, name);
        }
    }
}
