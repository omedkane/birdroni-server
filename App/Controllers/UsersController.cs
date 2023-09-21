using Birdroni.Resources;
using Birdroni.Models;
using Birdroni.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
    public ActionResult<object> Get()
    {
        return new { name = "Marco" };
    }

    // [HttpGet]
    // public async Task<ActionResult<List<User>>> GetAll()
    // {
    //     var allUsers = await _service.GetAllAsync();
    //     return allUsers;
    // }
    [HttpPost("login")]
    public async Task<ActionResult<object>> Login([FromForm] LoginResource login)
    {
        var user = await _service.GetUserAsync(login.email);

        if (user is null)
            return BadRequest(
                new
                {
                    error = "This user doesn't exist, check your credentials or consider creating a new account !"
                }
            );

        if (PwdHasher.Match(login.password, user.Salt, user.HashedPassword))
            return new { message = "You have been successfully logged-in !" };
        else
            return new { message = "Wrong password or email !" };
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create([FromForm] RegisterResource reg)
    {
        if (!TryValidateModel(reg))
            return ValidationProblem();

        User existingUser = await _service.GetUserAsync(reg.Email);

        if (existingUser is not null)
            return BadRequest(new { error = "This user already exists" });

        byte[] salt = PwdHasher.GenerateSalt();
        User newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = reg.Email,
            Firstname = reg.Firstname,
            Lastname = reg.Lastname,
            Avatar = reg.Avatar,
            HashedPassword = PwdHasher.HashPassword(reg.Password, salt),
            Salt = salt
        };

        await _service.CreateAsync(newUser);

        return newUser;
    }
}
