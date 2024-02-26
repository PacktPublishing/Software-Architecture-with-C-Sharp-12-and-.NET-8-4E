using DDD.ApplicationLayer;
using Microsoft.AspNetCore.Localization;
using PackagesManagement.Controllers;
using PackagesManagementDB;
using PackagesManagementDB.Extensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbLayer(builder.Configuration?.GetConnectionString("DefaultConnection")??string.Empty,
    "PackagesManagementDB");

builder.Services.AddAllQueries(typeof(ManagePackagesController).Assembly);
builder.Services.AddAllCommandHandlers(typeof(ManagePackagesController).Assembly);
builder.Services.AddAllEventHandlers(typeof(ManagePackagesController).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
var ci = new CultureInfo("en-US");

// Configure the Localization middleware
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ci),
    SupportedCultures = new List<CultureInfo>
                {
                    ci,
                },
    SupportedUICultures = new List<CultureInfo>
                {
                    ci,
                }
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
await SeedDatabase(app.Services);
app.Run();

static async Task SeedDatabase(IServiceProvider serviceProvider)
{
    using var serviceScope = serviceProvider.CreateScope();
    var context = serviceScope.ServiceProvider.GetRequiredService<MainDbContext>();

    await context.Seed(serviceScope);
}
