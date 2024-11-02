using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Listifyr
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<Microsoft.Maui.Controls.Toolbar, Microsoft.Maui.Handlers.ToolbarHandler>();
                    
                    Microsoft.Maui.Handlers.ToolbarHandler.Mapper.AppendToMapping("CustomNavigationView", (handler, view) =>
                    {
#if ANDROID
                        handler.PlatformView.ContentInsetStartWithNavigation = 200;
                        handler.PlatformView.SetContentInsetsAbsolute(200, 0);
#endif
                    });
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
