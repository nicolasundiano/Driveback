using Application.Users.Authentication.Commands.Register;
using Application.Users.Authentication.Commands.SignIn;
using Application.Users.Current.Commands.UpdateCurrentUser;
using Application.Users.Current.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.Users;

[Route("[controller]/[action]")]
public class AuthController : ApiController
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var registerResult = await _mediator.Send(command);

        return registerResult.Match(Ok, Problem);
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInUserCommand command)
    {
        var signInResult = await _mediator.Send(command);

        return signInResult.Match(Ok, Problem);
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