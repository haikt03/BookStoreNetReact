using BookStoreNetReact.Application.Options;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Infrastructure.Data;
using BookStoreNetReact.Infrastructure.Repositories;
using BookStoreNetReact.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStoreNetReact.Application.Helpers;
using FluentValidation.AspNetCore;
using FluentValidation;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Application.Interfaces.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using BookStoreNetReact.Infrastructure.Identity;

namespace BookStoreNetReact.Api.Extensions
{
    public static class AppServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Initial services
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen
            (c =>
                {
                    var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        BearerFormat = "JWT",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Description = "Put Bearer + your token in the box below",
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement{{ jwtSecurityScheme, Array.Empty<string>() }});
                }
            );

            // DbContext
            var databaseOptions = configuration.GetSection(DatabaseOptions.DatabaseSettings).Get<DatabaseOptions>();
            if (databaseOptions == null)
                throw new NullReferenceException(nameof(databaseOptions));
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(databaseOptions.ConnectionString, opt =>
                {
                    opt.CommandTimeout(databaseOptions.CommandTimeout);
                });
            });

            // Identity
            services
                .AddIdentityCore<AppUser>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<VietnameseIdentityErrorDescriber>();

            // Authentication, authorization
            var jwtOptions = configuration.GetSection(JwtOptions.JwtSettings).Get<JwtOptions>();
            if (jwtOptions == null)
                throw new NullReferenceException(nameof(jwtOptions));
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.TokenKey))
                    };
                });
            services.AddAuthorization();

            // Options
            services
                .AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.JwtSettings)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services
                .AddOptions<CloudOptions>()
                .BindConfiguration(CloudOptions.CloudinarySettings)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services
                .AddOptions<EmailOptions>()
                .BindConfiguration(EmailOptions.EmailSettings)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services
                .AddOptions<SmsOptions>()
                .BindConfiguration(SmsOptions.TwilioSettings)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<ICloudUploadService, CloudUploadService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISmsService, SmsService>();

            // Others
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(CreateBookDto).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddCors();
        }
    }
}
