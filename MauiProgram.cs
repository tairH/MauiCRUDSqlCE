using MauiCRUDSqlCE.Services;
using MauiCRUDSqlCE.ViewModels;
using UraniumUI;
using CommunityToolkit.Maui;

namespace MauiCRUDSqlCE;
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
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        })
            .ConfigureMauiHandlers(handlers =>
        {
            handlers.AddUraniumUIHandlers(); // 👈 This line should be added.
        })
            .UseMauiCommunityToolkit();
        ;

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddSingleton<IDataService, SqlCEDataService>();
        return builder.Build();
    }
}