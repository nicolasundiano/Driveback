using Application.Admins.Authentication.Commands.Register;
using Application.Admins.Authentication.Commands.SignIn;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers.Admins;

[Route("[controller]/[action]")]
public class AdminsAuthController : ApiController
{
    private readonly ISender _mediator;

    public AdminsAuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterAdminCommand command)
    {
        var errorOrRegister = await _mediator.Send(command);

        return errorOrRegister.Match(Ok, Problem);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(SignInAdminCommand command)
    {
        var errorOrSignIn = await _mediator.Send(command);

        return errorOrSignIn.Match(Ok, Problem);
    }
}