using Microsoft.EntityFrameworkCore;
using UsersMinimalAPI;
using System.Configuration;
using UsersMinimalAPI.Repositaries;
using System.Net.NetworkInformation;
using MediatR;
using UsersMinimalAPI.Mediatr.Queries;
using UsersMinimalAPI.Authentication;
using UsersMinimalAPI.Mediatr.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IUsersCommandRepositary, UsersCommandRepositary>();
builder.Services.AddScoped<IUsersQueryRepositary,UsersQueryRepositary>();
builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/users", async (IMediator mediator) =>
{    
    var result = await mediator.Send(new GetAllUsersQuery());
    if (result.IsSuccess)
    {
        return Results.Ok(result.Data.Select(x => new UsersDTO(x)));
    }    
    return Results.BadRequest();    
});

app.MapGet("/api/users/{id}", async (int id, IMediator mediator) =>
{
    var result = await mediator.Send(new GetUserQuery() { Id = id });
    if (result.IsSuccess)
    {
        return Results.Ok(new UsersDTO(result.Data));
    }
    return Results.NotFound();
});

app.MapPost("/api/users", async (UsersDTO usersDTO, IMediator mediator) =>
{
    var result = await mediator.Send(new CreateUserCommand() { UsersDTO = usersDTO });
    if (result.IsSuccess)
    {
        return Results.Created($"/api/users/{result.Data.Id}", result.Data);
    }
    return Results.BadRequest(result.Message);
});

app.MapPut("/api/users/{id}", async (int id, UsersDTO usersDTO, IMediator mediator) =>
{
    if (id != usersDTO.Id)
    {
        return Results.BadRequest();
    }

    // Implement logic to update an existing user
    // Example: await mediator.Send(new UpdateUserCommand(id, usersDTO));
    return Results.NoContent();
});

app.MapDelete("/api/users/{id}", async (int id, IMediator mediator) =>
{
    // Implement logic to delete a user by ID
    // Example: await mediator.Send(new DeleteUserCommand(id));
    return Results.NoContent();
});

app.MapPost("/api/users/authorize", async (IMediator mediator) =>
{
    // Implement authorization logic using IMediator
    // Example: var isAuthorized = await mediator.Send(new AuthorizeUserCommand());
    // return Results.Ok(isAuthorized);
    return Results.Ok();
});

app.Run();
