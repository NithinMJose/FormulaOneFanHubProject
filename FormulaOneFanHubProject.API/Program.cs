using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<AppDBContext>(
    context
    => context.UseSqlServer("Server=DESKTOP-389ELIT\\SQLEXPRESS01;Database=FormulaOneFanHubNET8;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;")
    );

builder.Services.AddIdentity<MyUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders().AddApiEndpoints();

//builder.Services.AddIdentityCore<MyUser>()
//    .AddEntityFrameworkStores<AppDBContext>().AddApiEndpoints();

    //.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<MyUser>();

app.Run();

class MyUser : IdentityUser { }

class AppDBContext :IdentityDbContext<MyUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {

    }
}