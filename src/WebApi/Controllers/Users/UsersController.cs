using Application.Users.Commands.AddChildUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetAll;
using Application.Users.Queries.GetUser;
using Infrastructure.Authentication.Common.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.Users;

[Route("[controller]/[action]")]
public class UsersController : ApiController
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var getUserResult = await _mediator.Send(new GetUserQuery());

        return getUserResult.Match(Ok, Problem);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        var updateUserResult = await _mediator.Send(command);

        return updateUserResult.Match(Ok, Problem);
    }
    
    [HttpPut]
    public async Task<IActionResult> AddChildUser(AddChildUserCommand command)
    {
        var addChildUserResult = await _mediator.Send(command);

        return addChildUserResult.Match(Ok, Problem);
    }
    
    [HttpGet]
    [MustHaveAdminRole]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}