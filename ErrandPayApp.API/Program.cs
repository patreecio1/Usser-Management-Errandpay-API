using ErrandPayApp.API.Configurations;
using ErrandPayApp.API;
using ErrandPayApp.Data.Utilities;
using ErrandPayApp.Infrastructure.Configuration;
using ErrandPayApp.Infrastructure.Filters;
using ErrandPayApp.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using ErrandPayApp.Core.Interfaces.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(config =>
{
    config.Filters.Add<GlobalExceptionFilter>();
})
.AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.IgnoreNullValues = true;
    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAutoMapper(typeof(AppUserProfile),
    typeof(DataUserProfile),
    typeof(InfrastructureUserProfile));

builder.Services.AddMemoryCache();
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AppServicesConfiguration(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddDocumentationConfiguration();
builder.Services.SchedulerServicesConfiguration(builder.Configuration);

string cors = builder.Configuration.GetValue<string>("cors");
builder.Services.AddCors(Options =>
{
    Options.AddPolicy("CorsPolicy",
        builder =>
        builder
            .WithOrigins(cors.Split(","))
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ErrandPay App service api v1");
        c.RoutePrefix = "";
    });
}
app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
