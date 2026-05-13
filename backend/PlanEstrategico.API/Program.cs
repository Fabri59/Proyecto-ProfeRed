using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using PlanEstrategico.API.Data;
using PlanEstrategico.API.Services;
using PlanEstrategico.API.Utilities;

var builder = WebApplication.CreateBuilder(args);

// =============== CONFIGURAR SERVICIOS ===============

// Base de datos
builder.Services.AddDbContext<PlanEstrategicoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// Autenticación JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JwtSettings:SecretKey is missing");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Autorización
builder.Services.AddAuthorization();

// Controladores
builder.Services.AddControllers();

// CORS
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins", policy =>
    {
        policy.WithOrigins(corsOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============== REGISTRAR SERVICIOS PERSONALIZADOS ===============

// Utilidades
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IMisionService, MisionService>();
builder.Services.AddScoped<IVisionService, VisionService>();
builder.Services.AddScoped<IValorService, ValorService>();
builder.Services.AddScoped<IObjetivoEstrategicoService, ObjetivoEstrategicoService>();
builder.Services.AddScoped<IAnalisisFODAService, AnalisisFODAService>();
builder.Services.AddScoped<IMatrizBCGService, MatrizBCGService>();
builder.Services.AddScoped<IAnalisisPorterService, AnalisisPorterService>();
builder.Services.AddScoped<IAnalisisPESTService, AnalisisPESTService>();
builder.Services.AddScoped<IEstrategiaIdentificacionService, EstrategiaIdentificacionService>();
builder.Services.AddScoped<IMatrizCAMEService, MatrizCAMEService>();
builder.Services.AddScoped<ICadenaValorService, CadenaValorService>();
builder.Services.AddScoped<IAutoCadenaValorService, AutoCadenaValorService>();
builder.Services.AddScoped<IAutoBCGService, AutoBCGService>();
builder.Services.AddScoped<IAutoPorterService, AutoPorterService>();
builder.Services.AddScoped<IResumenEjecutivoService, ResumenEjecutivoService>();
builder.Services.AddScoped<IReporteFinalService, ReporteFinalService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// =============== CONSTRUIR APLICACIÓN ===============

var app = builder.Build();

// =============== CONFIGURAR MIDDLEWARE ===============

// CORS
app.UseCors("AllowedOrigins");

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Plan Estratégico API V1");
    });
}

// HTTPS Redirect
app.UseHttpsRedirection();

// Autenticación y Autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// =============== EJECUTAR MIGRACIONES ===============

// Crear base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlanEstrategicoDbContext>();
    try
    {
        db.Database.Migrate();
        Console.WriteLine("✓ Base de datos migrada exitosamente");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error al migrar la base de datos: {ex.Message}");
        // Opcionalmente, puedes relanzar la excepción
        // throw;
    }
}

// =============== INICIAR APLICACIÓN ===============

app.Run();
