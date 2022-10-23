using Application.Common.Constant;
using Application.Commons;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using FluentValidation;
using MediatR;
using Infrastructure.Persistence.Repositories;
using Application.Interfaces.Token;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        /*if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("OneLoginDb"));
        }
        else
        {
            string connectionString = configuration.GetConnectionString(ApplicationConstant.DB_CONTEXT_NAME);
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(connectionString, serverVersion,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }*/
        string connectionString = configuration.GetConnectionString(ApplicationConstant.DB_CONTEXT_NAME);
        var serverVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySql(connectionString, serverVersion,
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddCors(options =>
        {
            options.AddPolicy("DefaultPolicy",
                policy =>
                {
                    policy.WithOrigins("https://localhost:7160")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
        });

        services.AddMvc()
                   .AddSessionStateTempDataProvider();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(60);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });


        //Add Unique Passwords

        services.AddIdentity<UserEntity, UserRoleEntity>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 2;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            // enable token generation and password reset
            .AddDefaultTokenProviders();
    

        //jwt settings
        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);
        services.AddSingleton(jwtSettings);

        //Authorization policy
        //services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CreateCormadoRight", policy =>
                  policy.RequireRole("InnoetechAdmin", "CormadoAdmin"));
        });

        services.AddAuthentication(
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                ).AddJwtBearer(
            options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; //TODO: Change to true in Production
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    NameClaimType = "name",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new
                    SymmetricSecurityKey(Encoding.UTF8.GetBytes
                    (jwtSettings.AccessTokenSecret!)),
                    ClockSkew = TimeSpan.Zero
                };
            });


        // Add the Authorization Feature to Swagger to login user with
        // valid token
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

        });


       /* services.AddIdentityServer()
            .AddApiAuthorization<CormadoEntity, ApplicationDbContext>();*/
       

        services.AddTransient<IDateTime, DateTimeService>();
  
        //DB Context
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //Repos
        services.AddScoped<IUserManager, UserManagerWrapper>();
        services.AddScoped<ICormadoRepository, CormadoRepository>();

        //Services 
        services.AddScoped<ITokenServices, TokenServices>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped<IIdentityService, IdentityService>();






        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
            options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));



        return services;
    }

}
