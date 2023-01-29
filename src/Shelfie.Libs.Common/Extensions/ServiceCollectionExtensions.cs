using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shelfie.Libs.Common.Helpers;
using Shelfie.Libs.Common.Middleware;
using Shelfie.Libs.Common.Services.DateTimeProvider;

namespace Shelfie.Libs.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShelfieControllers<TStartup>(this IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddSwagger()
            .AddValidation<TStartup>()
            .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
            // .AddMediatR(typeof(Startup))
            // todo .AddAuthentication()
            ;

        // Exception handling
        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(_ =>
        {
            // _.AddSecurityDefinition(
            //     "Bearer",
            //     new OpenApiSecurityScheme
            //     {
            //         Name = "Authorization",
            //         Description = "Access token",
            //
            //         In = ParameterLocation.Header,
            //         Type = SecuritySchemeType.Http,
            //
            //         BearerFormat = "JWT",
            //         Scheme = "bearer"
            //     });
            // _.AddSecurityRequirement(
            //     new OpenApiSecurityRequirement
            //     {
            //         {
            //             new OpenApiSecurityScheme
            //             {
            //                 Reference = new OpenApiReference
            //                 {
            //                     Type = ReferenceType.SecurityScheme,
            //                     Id = "Bearer"
            //                 },
            //                 Scheme = "oauth2",
            //                 Name = "Bearer",
            //                 In = ParameterLocation.Header
            //             },
            //             new List<string>()
            //         }
            //     });

            _.SwaggerDoc(
                AssemblyHelper.EntryAssemblyVersion,
                new OpenApiInfo
                {
                    Title = AssemblyHelper.EntryAssemblyName,
                    Version = AssemblyHelper.EntryAssemblyVersion
                });
            _.IncludeXmlComments(
                Path.Combine(AppContext.BaseDirectory, $"{AssemblyHelper.EntryAssemblyName}.xml")
            );
        });

        return services;
    }

    public static IServiceCollection AddValidation<TStartup>(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            // .AddValidatorsFromAssemblyContaining<TStartup>()
            ;

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        // TODO: Make a call to remote vault to get the issuer and issuer key.
        const string issuer = "shelfie-api-users";
        const string issuerKey = "shelfie-token-source";

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(_ =>
            {
                _.RequireHttpsMetadata = true;
                _.SaveToken = true;
                _.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        new HMACSHA256(
                            Encoding.UTF8.GetBytes(issuerKey)
                        ).Key
                    ),

                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                };
            })
            ;

        return services;
    }

    public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
    {
        services
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            ;

        return services;
    }
}
