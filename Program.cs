using System.Text;
using MasrafTakipApi.Data;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Service;    // <-- eklendi
using MasrafTakipApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
});

// 2) Identity & JWT
builder.Services.AddIdentity<User, IdentityRole<Guid>>(opts =>
    {
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// JWT setup
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidIssuer              = jwtSection["Issuer"],
        ValidateAudience         = true,
        ValidAudience            = jwtSection["Audience"],
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey         = new SymmetricSecurityKey(key)
    };
});

// 3) Role policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy =>
        policy.RequireRole(UserRole.Admin.ToString()));
    options.AddPolicy("RequirePersonnel", policy =>
        policy.RequireRole(UserRole.Personnel.ToString()));
});

// 4) DI registrations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExpenseRequestService, ExpenseRequestService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
