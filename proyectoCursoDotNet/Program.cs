using System.Data.Common;
using System.Security.Claims;
using APIconDB.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoCursoDotNet;
using Microsoft.OpenApi.Models;
using proyectoCursoDotNet.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(defaultScheme: "Bearer").AddJwtBearer();
builder.Services.AddControllers();

// Conditionally register UsersDbContext with a single database provider
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");

builder.Services.AddDbContext<UsersDbContext>(
    options => options.UseSqlite("Data Source=../APIconDB/Identifier.sqlite"));
    
builder.Services.AddDbContext<TasksDbContext>((
    options => options.UseSqlite("Data Source=../APIconDB/Identifier.sqlite")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/login", async (ClaimsPrincipal claims, [FromServices] UsersDbContext context) =>
    {
        try
        {
            var claim = claims.Claims.FirstOrDefault(c => c.Type == "Id");
            if (claim == null)
            {
                return Results.BadRequest("User ID claim not found.");
            }

            if (!int.TryParse(claim.Value, out int userId))
            {
                return Results.BadRequest("Invalid User ID claim.");
            }
            var user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                return Results.NotFound("User not found.");
            }

            return Results.Ok(user);
        }
        catch (Exception ex)
        {
            // Log the exception and return a generic error response
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while processing the request.");
            return Results.Problem("An error occurred while processing your request. Please try again later.");
        }    
    }
).RequireAuthorization();
app.MapGet("/maintenance", () => Results.Ok("The system is under maintenance. Please try again later."));
app.UseMiddlewareExtensionHandler();
app.MapControllers();

app.Run();

//TODO: 
// 1. Un informe que describa la arquitectura del proxy, las decisiones de diseño y los desafíos enfrentados.
// 2. Diagrama de la arquitectura, mostrando los componentes principales: proxy, servicios externos, base de datos (opcional).