using Oracle.ManagedDataAccess.Client;

namespace ContactBookLibrary
{
    public class ContactBussiness
    {
        OracleConnection oracleConnection = new OracleConnection("User Id=mars;Password=mars;Data Source=192.168.169.55:1600/IND83;");

        /// <summary>
        /// Adds a new contact to the contact book.
        /// </summary>
        /// <param name="contact">A tuple containing the name, phone number, and email of the contact to be added.</param>
        /// <returns>True if the contact was successfully added, false otherwise.</returns>
        public bool AddContactToDatabase(Contact contact)
        {
            try
            {
                // Create a new OracleCommand object.
                OracleCommand command = new OracleCommand();

                // Set the command's connection to the current oracleConnection.
                command.Connection = oracleConnection;

                // Set the command's text to the SQL statement for inserting a new contact into the contactbook table.
                command.CommandText = "INSERT INTO contactbook (person_name, person_number, person_email) VALUES (:name, :phonenumber, :email)";

                // Add the name, phone number, and email of the contact as parameters to the command.
                command.Parameters.Add(":name", OracleDbType.Varchar2).Value = contact.PersonName;
                command.Parameters.Add(":phonenumber", OracleDbType.Int64).Value = contact.PersonNumber;
                command.Parameters.Add(":email", OracleDbType.Varchar2).Value = contact.PersonEmail;

                // Open the oracleConnection.
                oracleConnection.Open();

                // Execute the command to insert the new contact into the contactbook table.
                command.ExecuteNonQuery();

                // Return true to indicate that the contact was successfully added.
                return true;
            }
            catch (OracleException ex)
            {
                // If an exception occurs, write an error message to the console and return false to indicate that the contact was not added.
                Console.WriteLine($"Failed to insert contact: {ex.Message}");
                return false;
            }
            finally
            {
                // Close the oracleConnection if it is not null.
                oracleConnection?.Close();
            }
        }
        public bool UpdateContactInDatabase(Contact contact)
        {
            try
            {
                // Create a new OracleCommand object.
                OracleCommand command = new OracleCommand();

                // Set the command's connection to the current oracleConnection.
                command.Connection = oracleConnection;

                // Set the command's text to the SQL statement for updating a contact in contactbook table
                //update tablde set (fields) into values() where.
                command.CommandText = "update contactbook set person_name = :c_name, person_number = :c_number, person_email = :c_email where person_id = :c_id";

                // Add the id, name, phone number, and email of the contact as parameters to the command.
                command.Parameters.Add(":C_name", OracleDbType.Varchar2).Value = contact.PersonName;
                command.Parameters.Add(":c_number1", OracleDbType.Int64).Value = contact.PersonNumber;
                command.Parameters.Add(":c_email", OracleDbType.Varchar2).Value = contact.PersonEmail;
                command.Parameters.Add(":c_id", OracleDbType.Int32).Value = contact.PersonId;

                // Open the oracleConnection.
                oracleConnection.Open();

                // Execute the command to insert the new contact into the contactbook table.
                command.ExecuteNonQuery();

                // Return true to indicate that the contact was successfully added.
                return true;
            }
            catch (OracleException ex)
            {
                // If an exception occurs, write an error message to the console and return false to indicate that the contact was not updated.
                Console.WriteLine($"Failed to update contact: {ex.Message}");
                return false;
            }
            finally
            {
                // Close the oracleConnection if it is not null.
                oracleConnection?.Close();
            }
        }

        /// <summary>
        /// Removes a contact with the specified ID from the contactbook table in the Oracle database.
        /// </summary>
        /// <param name="id">The ID of the contact to remove.</param>
        /// <returns>True if the contact was removed successfully, false otherwise.</returns>
        public bool RemoveContactFromDatabase(int id)
        {
            try
            {
                OracleCommand command = new OracleCommand();
                command.Connection = oracleConnection;
                command.CommandText = "delete from contactbook where person_id= :id";
                command.Parameters.Add(":id", OracleDbType.Int32).Value = id;

                oracleConnection.Open();
                command.ExecuteNonQuery();

                return true;
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Failed to remove contact: {ex.Message}");
                return false;
            }
            finally
            {
                oracleConnection?.Close();
            }
        }

        /// <summary>
        /// Removes all contacts from the contactbook table and reseeds the person_id column to start from 1.
        /// </summary>
        /// <returns>True if successful, false if an exception was thrown.</returns>
        public bool TruncateContactbookTableAndReseedPersonId()
        {
            try
            {
                // Create commands for truncating the table and reseeding the identity column
                OracleCommand truncateCommand = new OracleCommand();
                OracleCommand reseedCommand = new OracleCommand();

                // Set the connection and SQL command for truncating the table
                truncateCommand.Connection = oracleConnection;
                truncateCommand.CommandText = "truncate table contactbook";

                // Set the connection and SQL command for reseeding the identity column
                reseedCommand.Connection = oracleConnection;
                reseedCommand.CommandText = "ALTER TABLE contactbook MODIFY person_id GENERATED BY DEFAULT AS IDENTITY (START WITH 1)";

                // Open the database connection
                oracleConnection.Open();

                // Execute the truncate and reseeding commands
                truncateCommand.ExecuteNonQuery();
                reseedCommand.ExecuteNonQuery();

                // Return true if successful
                return true;
            }
            catch (OracleException ex)
            {
                // Log the exception message and return false
                Console.WriteLine($"Failed to truncate and reseed contactbook table: {ex.Message}");
                return false;
            }
            finally
            {
                // Close the database connection
                oracleConnection?.Close();
            }
        }

        /// <summary>
        /// Finds the contacts from the contactbook table and stores them in a list.
        /// </summary>
        /// <param name="contacts">Reference to the list to store the contacts in.</param>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        public bool FindContacts(ref List<Contact> contacts, string SearchedName)
        {
            OracleDataReader reader = null;
            try
            {
                // Create a new Oracle command
                OracleCommand command = new OracleCommand();
                command.Connection = oracleConnection;

                // Set the command text to select all data from the contactbook table
                command.CommandText = "SELECT * FROM contactbook WHERE contactbook.person_name LIKE '%' || :SearchedName || '%'";
                command.Parameters.Add(":SearchedName", OracleDbType.Varchar2).Value = SearchedName;

                // Open the database connection
                oracleConnection.Open();

                // Execute the command and store the results in a reader
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                // Loop through each row in the reader and create a new Contact object for each
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["person_id"]);
                    string name = (string)reader["person_name"];
                    long number = Convert.ToInt64(reader["person_number"]);
                    string email = (string)reader["person_email"];

                    contacts.Add(new Contact(id, name, number, email));
                }

                // Return true to indicate success
                return true;
            }
            catch (OracleException ex)
            {
                // Log any errors and return false to indicate failure
                Console.WriteLine($"Failed to retrieve contacts: {ex.Message}");
                return false;
            }
            finally
            {
                // Close the reader and database connection (if they're not null)
                reader?.Close();
                oracleConnection?.Close();
            }
        }

        /// <summary>
        /// Retrieves all contacts from the contactbook table and stores them in a list.
        /// </summary>
        /// <param name="contacts">Reference to the list to store the contacts in.</param>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        public bool RetrieveContacts(ref List<Contact> contacts)
        {
            OracleDataReader reader = null;
            try
            {
                // Create a new Oracle command
                OracleCommand command = new OracleCommand();
                command.Connection = oracleConnection;

                // Set the command text to select all data from the contactbook table
                command.CommandText = "SELECT * FROM contactbook";

                // Open the database connection
                oracleConnection.Open();

                // Execute the command and store the results in a reader
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                // Loop through each row in the reader and create a new Contact object for each
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["person_id"]);
                    string name = (string)reader["person_name"];
                    long number = Convert.ToInt64(reader["person_number"]);
                    string email = (string)reader["person_email"];

                    contacts.Add(new Contact(id, name, number, email));
                }

                // Return true to indicate success
                return true;
            }
            catch (OracleException ex)
            {
                // Log any errors and return false to indicate failure
                Console.WriteLine($"Failed to retrieve contacts: {ex.Message}");
                return false;
            }
            finally
            {
                // Close the reader and database connection (if they're not null)
                reader?.Close();
                oracleConnection?.Close();
            }
        }
    }
}
