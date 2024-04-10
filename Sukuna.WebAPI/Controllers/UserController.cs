using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sukuna.Business.Interfaces;
using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using Sukuna.Service.Services;

namespace Sukuna.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
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

        // Vérifie si user existe à partir du nom
        var users = _userService.UserExists(userCreate);

        if (users)
        {
            ModelState.AddModelError("", "User doesn't exists or User already exists");
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

    [HttpGet("{userId}/userOrders")]
    public IActionResult GetLinesByUser(int userId)
    {
        if (!_userService.UserExistsById(userId))
            return NotFound();

        var userOrders = _mapper.Map<List<SupplierOrderResource>>(
            _userService.GetSupplierOrdersByUser(userId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(userOrders);
    }

    [HttpPost("{userEmail},{userMdp}")]
    [ProducesResponseType(200, Type = typeof(UserResource))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult GetAuthauthUser(string userEmail, string userMdp)
    {
        var user = _userService.GetAuthauthUser(userEmail, userMdp);

        if (user == null)
            return Unauthorized(); // L'authentification a échoué

        var userResource = _mapper.Map<UserResource>(user);

        return Ok(userResource); // Authentification réussie, retourne les données du user
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(200, Type = typeof(User))]
    [ProducesResponseType(400)]
    public IActionResult GetUserById(int userId)
    {
        if (!_userService.UserExistsById(userId))
            return NotFound();

        var user = _mapper.Map<UserResource>(_userService.GetUserById(userId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(user);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = _mapper.Map<List<UserResource>>(_userService.GetUsers());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult UpdateUser(int userId, [FromBody] UserResource updatedUser)
    {
        if (updatedUser == null)
            return BadRequest(ModelState);

        if (userId != updatedUser.ID)
            return BadRequest(ModelState);

        if (!_userService.UserExistsById(userId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var userMap = _mapper.Map<User>(updatedUser);

        if (!_userService.UpdateUser(userMap))
        {
            ModelState.AddModelError("", "Something went wrong updating owner");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully Updated");
    }


    [HttpDelete("{userId}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteUser(int userId)
    {
        if (!_userService.UserExistsById(userId))
        {
            return NotFound();
        }

        var userToDelete = _userService.GetUserById(userId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_userService.DeleteUser(userToDelete))
        {
            ModelState.AddModelError("", "Something went wrong deleting user");
        }

        return Ok("Successfully deleted");
    }
}