using System.Configuration;
using System.Net;
using Microsoft.EntityFrameworkCore;
using RoutineMaster.Data;
using RoutineMaster.Service;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RMDataContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMvcCore();

builder.Services.AddTransient<IEducationService, EducationService>();
builder.Services.AddTransient<IMundaneService, MundaneService>();
builder.Services.AddTransient<IHealthService, HealthService>();
builder.Services.AddTransient<IFinanceService, FinanceService>();
builder.Services.AddTransient<ICreativeProjectService, CreativeProjectService>();
builder.Services.AddTransient<IScoreService, ScoreService>();

// builder.Services.AddAuthentication(opt => {
//     opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = "https://localhost:5001",
//             ValidAudience = "https://localhost:5001",
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("51cv$$3233451frf234132ffwaf"))
//         };
//     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.MapControllers();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseForwardedHeaders(new ForwardedHeadersOptions{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


app.UseAuthentication();
app.UseAuthorization();
app.Run();
