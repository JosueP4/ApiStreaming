using APIStreaming.Models;
using APIStreaming.Services;
using APIStreaming.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add connection DB
builder.Services.AddDbContext<ApistreamingDbContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("conexionSQL")));

//add services
builder.Services.AddScoped<UsuarioServicios>();
builder.Services.AddScoped<SuscripcionesServices>();
builder.Services.AddScoped<PlaneService>();
builder.Services.AddScoped<PagoServices>();
builder.Services.AddScoped<ContenidoServices>();
builder.Services.AddScoped<NotificacionService>();
builder.Services.AddHostedService<BackgroundNotificacion>();

builder.Logging.AddConsole();


//add Json Web Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones =>
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
        
    });

//builder.Services.AddAuthentication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
