using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/sukuna")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]

    public IActionResult CreateUser([FromBody] UserResource userCreate)
    {
        if (userCreate == null)
            return BadRequest(ModelState);

        var users = _userService.GetUserTrimToUpper(userCreate);

        if (users != null)
        {
            ModelState.AddModelError("", "Owner already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userMap = _mapper.Map<User>(userCreate);

        if (!_userService.CreateUser(userMap))
        {
            ModelState.AddModelError("", "Something went wrong while savin");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}