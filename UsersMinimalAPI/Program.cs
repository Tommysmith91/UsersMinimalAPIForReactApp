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
    var result = await mediator.Send(new UpdateUserCommand() { UsersDTO = usersDTO,Id = id });
    if (result.IsSuccess)
    {
        return Results.NoContent();
    }    
    return Results.BadRequest(result.Message);
});

app.MapDelete("/api/users/{id}", async (int id, IMediator mediator) =>
{
    var result = await mediator.Send(new DeleteUserCommand() { Id = id });
    if (result.IsSuccess)
    {
        return Results.NoContent();
    }    
    return Results.BadRequest(result.Message);
});

app.MapPost("/api/users/authorize", async (IMediator mediator) =>
{
    
    return Results.Ok();
});

app.Run();
