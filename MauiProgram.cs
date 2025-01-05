using Microsoft.Extensions.Logging;
using TransactionTracker.Services;
using MudBlazor;
using MudBlazor.Services;


namespace TransactionTracker
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
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<IInflowService, InflowService>();
            builder.Services.AddScoped<ITagsService, TagsService>();
            builder.Services.AddScoped<IDebtService, DebtService>();



#if DEBUG
            builder.Services.AddMudServices();

            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
