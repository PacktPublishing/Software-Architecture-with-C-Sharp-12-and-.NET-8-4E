using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PackagesManagementBlazor.Client;
using PackagesManagementBlazor.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new 
    HttpClient { BaseAddress = new Uri("https://localhost:7269/") });
builder.Services.AddScoped<PackagesClient>();
await builder.Build().RunAsync();
