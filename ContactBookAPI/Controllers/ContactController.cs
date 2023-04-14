using System.Collections.Generic;
using ContactBook;
using ContactBookLibrary.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

// Define your API controller class
[ApiController]
[Route("api/contacts")]
public class ContactsController : ControllerBase
{
    readonly ContactBussiness contactBussiness = new ContactBussiness();

    [HttpGet]
    public ActionResult<IEnumerable<Contact>> GetAllContacts()
    {
        try
        {
            List<Contact>? contacts = contactBussiness.RetrieveContacts();
            return Ok(contacts);
        }catch(NoDataFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            return Content(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult AddContact([FromBody]Contact contact)
    {
        try
        {
            contactBussiness.TruncateContactbookTableAndReseedPersonId();
            contactBussiness.AddContactToDatabase(contact);
            return Ok("Contact Added Successfully");
        }catch(NoDataFoundException ex)
        {
            return NotFound(ex.Message);
        }catch (Exception ex)
        {
            Response.StatusCode = 500;
            return Content(ex.Message);
        }
    }

    [HttpDelete("id")]
    public IActionResult DeleteContactById(int id)
    {
        try
        {
            contactBussiness.RemoveContactFromDatabase(id);
            return Ok("Contact Deleted Successfully");
        }
        catch(OracleException  ex)
        {
            Response.StatusCode = 500;
            return Content(ex.Message);
        }
    }

   /* [HttpPut("{id}")]
    public IActionResult UpdateContactById([FromBody]int id, Contact contact)
    {
        try
        {
            contactBussiness.UpdateContactInDatabase(contact);
            return Ok("Contact Updated Successfully");
        }catch(NoDataFoundException ex)
        {
            return NotFound(ex.Message) ;
        }
        catch(OracleException ex)
        {
            Response.StatusCode = 500;
            return Content(ex.Message);
        }
    }
   */
}