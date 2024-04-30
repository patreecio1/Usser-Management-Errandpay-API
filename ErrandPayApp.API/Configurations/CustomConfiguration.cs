using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ErrandPayApp.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ErrandPayApp.Infrastructure.Utilities;

namespace ErrandPayApp.API.Configurations
{
    public static class CustomConfiguration
    {
        public static void AddIdentityConfiguration(this IServiceCollection services,
   IConfiguration configuration)
        {
            AuthConfig _authConfig = configuration.GetSection("AuthConfig").Get<AuthConfig>();

            services.AddAuthentication("Bearer")
            .AddJwtBearer(opts =>
            {
                var Key = Encoding.UTF8.GetBytes(_authConfig.Key);
                //opts.Authority = _authConfig.Url;
                opts.RequireHttpsMetadata = false;
                opts.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _authConfig.Issuer,
                    ValidAudience = _authConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
                opts.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async context =>
                    {
                        Exception ex = context.Exception;

                        var result = ResponseModel<string>.Failed("", "Authentication failed");
                        try
                        {

                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";
                        }
                        catch (Exception exx)
                        {
                            Log<string>.LogError(exx);
                        }

                        var responseJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        await context.Response.WriteAsync(responseJson);
                    },
                    OnChallenge = async context =>
                    {
                        Exception ex = context.AuthenticateFailure;

                        var result = ResponseModel<string>.Failed("", "Authentication failed");
                        try
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";


                            var responseJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            await context.Response.WriteAsync(responseJson);
                        }
                        catch (Exception exx)
                        {
                            Log<string>.LogError(exx);
                        }
                    }
                };
            });
        }

        public static void AddDocumentationConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ErrandPay App Service",
                    Description = "ErrandPay App service api",
                    Contact = new OpenApiContact
                    {
                        Email = "ErrandPay.com",
                        Name = "Admin Admin",
                        Url = new Uri("http://ErrandPay.com")
                    }
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                OpenApiSecurityRequirement openSecurityReq = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                };
                c.AddSecurityRequirement(openSecurityReq);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }


    }
}
