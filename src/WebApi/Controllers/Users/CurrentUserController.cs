using Application.Users.Current.Commands.UpdateCurrentUser;
using Application.Users.Current.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.Users;

[Microsoft.AspNetCore.Components.Route("[controller]/[action]")]
public class CurrentUserController : ApiController
{
    private readonly ISender _mediator;

    public CurrentUserController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser()
    {
        var currentUserResult = await _mediator.Send(new GetCurrentUserQuery());

        return currentUserResult.Match(Ok, Problem);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCurrentUser(UpdateCurrentUserCommand command)
    {
        var updateCurrentUserResult = await _mediator.Send(command);

        return updateCurrentUserResult.Match(Ok, Problem);
    }
}