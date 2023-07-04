using System.Text.Json.Serialization;
using WebApi.Helpers;
using WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Necesario añadir el paquete 
//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.19

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;
 
    services.AddDbContext<DataContext>();


    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ITokenService, TokenService>();

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwt=>{
        jwt.TokenValidationParameters = new TokenValidationParameters{
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("This is my custom Secret key for authentication")),
            ValidateLifetime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        jwt.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Headers["Authorization"];
                    return Task.CompletedTask;
                },
            };
    });
/*
    services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    }); */
}

var app = builder.Build();

// configure HTTP request pipeline
{
    app.UseRouting();
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();

    //app.UseSession();

    app.MapControllers();
}

app.Run("http://localhost:4000");