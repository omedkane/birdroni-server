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
    public ActionResult<object> Login([FromForm] LoginResource login)
    {
        // var hasher = new PasswordHasher<User>();
        // var user = await _service.GetUserAsync(login.email);
        // var verificationResult = hasher.VerifyHashedPassword(
        //     user,
        //     user.HashedPassword,
        //     login.password
        // );
        // var response = verificationResult;
        return login;
        // _service.GetUserAsync()
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create([FromForm] RegisterResource reg)
    {
        bool val = TryValidateModel(reg);
        if (!val)
            return new { message = val.ToString() };

        User existingUser = await _service.GetUserAsync(reg.Email);

        if (existingUser is not null)
            return BadRequest(new { error = "This user already exists" });

        User newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = reg.Email,
            Firstname = reg.Firstname,
            Lastname = reg.Lastname,
            Avatar = reg.Avatar,
            HashedPassword = PwdHasher.HashPassword(reg.Password)
        };

        if (!TryValidateModel(newUser))
            return ValidationProblem();

        await _service.CreateAsync(newUser);

        return newUser;
    }
}
