using System.Reflection;
using System.Text;
using AuthControl;
using AuthControl.Services;
using AuthControl.Services.DerivSmartRobot.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var connString = builder.Configuration.GetConnectionString("DefaultConnection");
var migrationAssembly = typeof(ApplicationContext).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddDbContext<ApplicationContext>(
    o => o.UseMySql(
        connString,
        new MySqlServerVersion(new Version(5, 6)),
        x => x.MigrationsAssembly(migrationAssembly)
            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<HotmartService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddScoped<DerivClient>();
builder.Services.AddLogging();
builder.Services.AddSendGrid(x =>
{
    x.ApiKey = "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";
});
builder.Host.ConfigureLogging(x =>
{
    x.ClearProviders();
    x.AddConsole();
});





var secreetkey = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtAuth").GetValue<string>("SecretKey"));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secreetkey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});





builder.Services.AddMvc();

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
