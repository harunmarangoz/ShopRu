using Business.Interfaces;
using Business.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;

namespace WebApi.Controllers;

[Route("api/user")]
public class UserController : BaseController
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult List()
    {
        return Ok(_userService.List());
    }
    
    [HttpPost("create")]
    public IActionResult Create([FromBody] UserDto model)
    {
        return Ok(_userService.Create(model));
    }
}