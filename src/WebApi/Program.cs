using Application;
using Infrastructure;
using WebApi;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .ConfigureApplication()
        .ConfigureInfrastructure(builder.Configuration)
        .ConfigureWebApi();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error");
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    await app.Services.InitializeInfrastructure();
    app.Run();
}