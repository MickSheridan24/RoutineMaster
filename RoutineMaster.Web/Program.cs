using System.Configuration;
using System.Net;
using Microsoft.EntityFrameworkCore;
using RoutineMaster.Data;
using RoutineMaster.Service;
using Microsoft.AspNetCore.HttpOverrides;

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




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseAuthorization();

app.MapControllers();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseForwardedHeaders(new ForwardedHeadersOptions{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Run();
