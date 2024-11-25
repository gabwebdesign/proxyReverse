using System.Security.Claims;
using proyectoCursoDotNet;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(defaultScheme: "Bearer").AddJwtBearer();
builder.Services.AddControllers();
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
builder.Services.AddHttpClient<IProxyService, ProxyService>("ChuckNorrisAPI", client => 
    client.BaseAddress = new Uri("https://api.hacienda.go.cr/"));
builder.Services.AddScoped<IProxyService, ProxyService>();
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

app.MapGet("/api/v1/proxy", () => "Hello World!");
app.MapGet("/login", (ClaimsPrincipal user) =>
    {
        var username = user.Identity?.Name ?? "Anonymous";
        return string.Format("Bienvenido : {0}", username);
    }
).RequireAuthorization();
app.MapControllers();

app.Run();