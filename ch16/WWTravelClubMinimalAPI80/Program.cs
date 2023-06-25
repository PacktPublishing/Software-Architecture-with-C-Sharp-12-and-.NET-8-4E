using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WWTravelClubMinimalAPI80.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<WWTravelClubDB.MainDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                 b => b.MigrationsAssembly("WWTravelClubDB")));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new() { Title = "WWTravelClub Minimal API - .NET 8", Version = "v2" });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "WWTravelClub Minimal API - .NET 8"));


app.UseHttpsRedirection();

app.MapGet("/api/packages", async ([FromServices] WWTravelClubDB.MainDBContext ctx,
        DateTime start, DateTime stop) =>
{
    try
    {
        var res = await ctx.Packages
            .Where(m => start >= m.StartValidityDate
            && stop <= m.EndValidityDate)
            .Select(m => new PackagesListDTO
            {
                StartValidityDate = m.StartValidityDate,
                EndValidityDate = m.EndValidityDate,
                Name = m.Name,
                DurationInDays = m.DurationInDays,
                Id = m.Id,
                Price = m.Price,
                DestinationName = m.MyDestination.Name,
                DestinationId = m.DestinationId
            })
            .ToListAsync();
        return Results.Ok(res);
    }
    catch (Exception)
    {
        return Results.StatusCode(500);
    }
})
.WithName("GetPackagesByDate")
.WithOpenApi();

app.Run();

