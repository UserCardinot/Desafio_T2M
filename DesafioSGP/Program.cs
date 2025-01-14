using DesafioSGP.Application.Services;
using DesafioSGP.Domain.Interfaces;
using DesafioSGP.Infrastructure.Repositories;
using DesafioSGP.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DesafioSGP.Domain.Entities;
using AutoMapper;
using DesafioSGP.Application.Mappings;  // Adicione a importação para o AutoMapper

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrando os repositórios e serviços
builder.Services.AddScoped<IUsersRepository, UserRepository>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();  // Mantém o repositório de Projeto

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjetoService>();  // Mantém o serviço de Projeto

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtUtils>();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ProjetoProfile>();  // Configuração do AutoMapper para Projeto
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Configuração do JSON para evitar ciclos de objetos
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve; // Previne ciclos
        options.JsonSerializerOptions.Converters.Add(new JsonDateOnlyConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Informe o token JWT no formato 'Bearer <token>'",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");
if (string.IsNullOrEmpty(jwtSettings["Secret"]) ||
    string.IsNullOrEmpty(jwtSettings["Issuer"]) ||
    string.IsNullOrEmpty(jwtSettings["Audience"]))
{
    throw new InvalidOperationException("Configurações JWT estão incompletas ou ausentes.");
}

var key = builder.Configuration["Jwt:Secret"];
if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("Chave JWT não encontrada.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = services.GetRequiredService<UserManager<User>>();

    await SeedRolesAndUsersAsync(roleManager, userManager);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowLocalhost");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
{
    string[] roleNames = { "User", "Admin" };

    foreach (var roleName in roleNames)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            role = new IdentityRole<Guid> { Name = roleName, NormalizedName = roleName.ToUpperInvariant() };
            await roleManager.CreateAsync(role);
        }
    }

    var adminEmail = "admin@example.com";
    var adminPassword = "Admin123!";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            Name = "Admin User"
        };

        var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    var userEmail = "user@example.com";
    var userPassword = "User123!";
    var defaultUser = await userManager.FindByEmailAsync(userEmail);

    if (defaultUser == null)
    {
        defaultUser = new User
        {
            UserName = userEmail,
            Email = userEmail,
            Name = "Default User"
        };

        var createUserResult = await userManager.CreateAsync(defaultUser, userPassword);
        if (createUserResult.Succeeded)
        {
            await userManager.AddToRoleAsync(defaultUser, "User");
        }
    }
}
