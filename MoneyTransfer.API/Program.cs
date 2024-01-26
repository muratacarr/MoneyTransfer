using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneyTransfer.Core.Options;
using MoneyTransfer.Core.Entities;
using MoneyTransfer.Core.Repositories;
using MoneyTransfer.Core.Services;
using MoneyTransfer.Core.UnitOfWork;
using MoneyTransfer.Repository.Contexts;
using MoneyTransfer.Repository.Repositories;
using MoneyTransfer.Repository.UnitOfWork;
using MoneyTransfer.Service.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using MoneyTransfer.Service.Validations;
using System.Reflection;
using MoneyTransfer.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(v =>
{
    v.ImplicitlyValidateChildProperties = true;
    v.ImplicitlyValidateRootCollectionElements = true;
    v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetUserAccountDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CloseAccountDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransferMoneyDtoValidator>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Con"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("MoneyTransfer.Repository");
    });
});

builder.Services.AddIdentity<AppUser,IdentityRole<int>>(option =>
{
    option.User.RequireUniqueEmail = true;
    option.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
