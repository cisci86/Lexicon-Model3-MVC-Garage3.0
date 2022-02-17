using Garage_2._0.Automapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Garage_2._0.Services;
using Garage_2._0.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GarageVehicleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GarageVehicleContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVehicleTypeSelectListService, VehicleTypeSelectListService>();

builder.Services.AddAutoMapper(typeof(GarageMappings));

var app = builder.Build();

app.SeedDataAsync().GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehicles}/{action=VehiclesOverview}/{id?}");

app.Run();
