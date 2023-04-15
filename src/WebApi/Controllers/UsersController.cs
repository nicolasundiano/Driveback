using Application.Users.Authentication.Commands.Login;
using Application.Users.Authentication.Commands.Register;
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

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var registerResult = await _mediator.Send(command);

        return registerResult.Match(Ok, Problem);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var loginResult = await _mediator.Send(command);

        return loginResult.Match(Ok, Problem);
    }
}