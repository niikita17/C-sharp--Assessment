using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Asp.Versioning;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection"),
                        sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }
    );
            });
        services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer =
                configuration["Jwt:Issuer"],

            ValidAudience =
                configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        configuration["Jwt:Key"]))
        };
});
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthRepository,
            AuthRepository>();



        services.AddScoped<IAuthService, AuthService>();

        services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion =
                new ApiVersion(1, 0);

            options.AssumeDefaultVersionWhenUnspecified = true;
        });
        return services;
    }
}