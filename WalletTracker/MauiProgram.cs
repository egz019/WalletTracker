using CommunityToolkit.Maui;
using Microcharts.Maui;
using WalletTracker.Controls;

#if __ANDROID__
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using WalletTracker.Platforms.Android;
#elif __IOS__
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using WalletTracker.Platforms.iOS;
#endif


using Microsoft.Maui.Controls.Handlers.Compatibility;


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
        AddHandlers();

        return builder.Build();
    }

    private static void AddHandlers()
    {
        Microsoft.Maui.Handlers.EntryHandler.ElementMapper.AppendToMapping(nameof(BorderedEntry), (handler, view)
         => 
         {
            #if __ANDROID__ || __IOS__
                if (view is BorderedEntry borderedEntry)
                {
                    EntryMapper.Map(handler, borderedEntry);
                }
            #endif
         });
    }
}
