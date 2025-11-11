using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SoftAPINew.Infrastructure.Interfaces;

using SoftAPINew.Models;

namespace SoftAPINew.Controllers;

[ApiController]
[Route("[controller]")]
    
public class ContactsController : ControllerBase 
{
    private readonly IDataRepository _repo;

    public ContactsController(IDataRepositoryFactory factory)
    {
        _repo = factory.Create("AP");
    }

    [HttpGet(Name = "GetContacts")]
    public async Task<List<Contact>> GetAll()
    {
        var results = new List<Contact>();
        var rows = await _repo.GetDataAsync("GetAllContactUpdates");


        foreach (var row in rows) 
        {
            var contact = new Contact {
                VendorID = row["VendorID"] != DBNull.Value ? Convert.ToInt32(row["VendorID"]) : 0,
                FirstName = row["FirstName"]?.ToString() ?? string.Empty,
                LastName = row["LastName"]?.ToString() ?? string.Empty,
            };

            results.Add(contact);
        }
        
        return results;
    }
}