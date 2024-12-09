using AutoRiaClone.Database;
using AutoRiaClone.Database.Auth;
using AutoRiaClone.DTOs;
using AutoRiaClone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services
    .AddIdentity<User, Role>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<DatabaseUtils>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<GeoService>();
builder.Services.AddScoped<AdvertisementService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.UseEndpoints(b =>
{
   
});
app.MapControllers();

app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // This makes the Swagger UI accessible at '/'.
});



app.Run();