using Microsoft.Extensions.Logging;
using PackagesManagementMAUIBlazor.Data;
using PackagesManagementMAUIBlazor.Services;

namespace PackagesManagementMAUIBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7215") });
            builder.Services.AddScoped<PackagesClient>();

            return builder.Build();
        }
    }
}