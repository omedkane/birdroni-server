using Birdroni.Resources;
using Birdroni.Models;
using Birdroni.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Birdroni.Misc.Security;

namespace Birdroni.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UsersService _service;
    private readonly JWToken _jwt;

    public AuthController(UsersService usersService, JWToken jwt)
    {
        _service = usersService;
        _jwt = jwt;
    }

    [AllowAnonymous]
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
        {
            var token = _jwt.GenerateToken(user.Id.ToString());
            return new { token, message = "You have been successfully logged-in !" };
        }
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
        User newUser =
            new()
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

        return new { token = _jwt.GenerateToken(newUser.Id.ToString()) };
    }
}
