using Birdroni.DTOs;
using Birdroni.Models;
using Birdroni.Services;
using Microsoft.AspNetCore.Mvc;

namespace Birdroni.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _service;

    public UsersController(UsersService usersService)
    {
        _service = usersService;
    }

    [HttpGet]
    public ActionResult<Object> Get()
    {
        return new { name = "Marco" };
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        var allUsers = await _service.GetAllAsync();
        return allUsers;
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create([FromForm] RegisterDTO reg)
    {
        var existingUser = await _service.GetUserAsync(reg.Email);

        if (existingUser is not null)
            return BadRequest(new { error = "This user already exists" });

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = reg.Email,
            Firstname = reg.Firstname,
            Lastname = reg.Lastname,
            Avatar = reg.Avatar
        };
        await _service.CreateAsync(newUser);

        return newUser;
    }
}
