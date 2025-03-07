using CommunityToolkit.Maui;
using Microcharts.Maui;

namespace WalletTracker;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism(PrismStartup.Configure)
            .UseMauiCommunityToolkit()
            .UseMicrocharts()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Raleway-Regular.ttf", "RalewayRegular");
                fonts.AddFont("Raleway-Semibold.ttf", "RalewaySemibold");
                fonts.AddFont("Raleway-Bold.ttf", "RalewayBold");
                fonts.AddFont("Raleway-LightItalic.ttf", "RalewayLightItalic");
                fonts.AddFont("Raleway-Italic.ttf", "RalewayItalic");
                fonts.AddFont("Raleway-Medium.ttf", "RalewayMedium");
                fonts.AddFont("Raleway-Thin.ttf", "RalewayThin");
            });

        //#if DEBUG
        //       builder.Logging.Do(builder.Services, LogLevel.Debug);
        //#endif

        return builder.Build();
    }
}
