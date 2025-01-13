using AutoMapper;
using DesafioSGP.Application.Mappings;
using DesafioSGP.Application.Services;
using DesafioSGP.Data;
using DesafioSGP.Domain.Interfaces;
using DesafioSGP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração da string de conexão
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");
}

// Configuração do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registro dos repositórios
builder.Services.AddScoped<IUsersRepository, UserRepository>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>(); // Agora sem passar a string de conexão diretamente

// Registro dos serviços
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjetoService>();
builder.Services.AddScoped<TarefaService>();

// AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<ProjetoProfile>();  // Certificando-se de que o perfil foi adicionado corretamente
}, AppDomain.CurrentDomain.GetAssemblies());



// Configuração dos controllers e serialização
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonDateOnlyConverter());
    });

// Configuração do Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
