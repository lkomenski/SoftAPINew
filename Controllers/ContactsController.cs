using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SoftAPINew.Models;

namespace SoftAPINew.Controllers;

[ApiController]
[Route("[controller]")]
    
public class ContactsController : ControllerBase 
{
    const string connectionString = "Server=LEENA-LAPTOP;Database=AP;User Id=AppUser;Password=password1;Encrypt=False;TrustServerCertificate=True;";
    SqlConnection _connection;

    public ContactsController()
    {
        _connection = new SqlConnection(connectionString);
        _connection.Open();
    }

    [HttpGet(Name = "GetContacts")]
    public List<Contact> GetAll()
    {
        var contacts = new List<Contact>();

        using (SqlCommand command = new SqlCommand("GetAllContactUpdates", _connection))
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var contact = new Contact
                    {
                        VendorID = reader["VendorID"] != DBNull.Value ? Convert.ToInt32(reader["VendorID"]) : 0,
                        LastName = reader["LastName"]?.ToString() ?? string.Empty,
                        FirstName = reader["FirstName"]?.ToString() ?? string.Empty
                    };
                    contacts.Add(contact);
                }
            }
        }
        return contacts;
     }
 }

public class Contact
{
    public int VendorID { get; set; } = 0;
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
}
