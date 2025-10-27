using Microsoft.AspNetCore.Mvc;
using SoftAPINew.Models;

namespace SoftAPINew.Controllers;

[ApiController]
[Route("[controller]")]

public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] LoginRequest loginrequest)
    {
        if (loginrequest == null || string.IsNullOrEmpty(loginrequest.Email) || string.IsNullOrEmpty(loginrequest.Password))
            return BadRequest("Email and Password are required.");

        return Ok(new User
        {
            Id = 1,
            Email = loginrequest.Email,
            FirstName = "John",
            LastName = "Doe"
        });
    }
}