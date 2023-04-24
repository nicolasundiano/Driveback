using Application.Users.Authentication.Commands.Login;
using Application.Users.Authentication.Commands.Register;
using Application.Users.Authentication.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers;

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
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var loginResult = await _mediator.Send(command);

        return loginResult.Match(Ok, Problem);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser()
    {
        var errorOrUser = await _mediator.Send(new GetCurrentUserQuery());

        return errorOrUser.Match(Ok, Problem);
    }
}