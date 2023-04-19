using Application.Users.Queries.GetAll;
using Infrastructure.Authentication.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers;

[Route("[controller]/[action]")]
public class UsersController : ApiController
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [MustHaveAdminRole]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}