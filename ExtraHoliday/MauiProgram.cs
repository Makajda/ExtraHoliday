﻿using MudBlazor.Services;

namespace ExtraHoliday; 
public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddMudServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddScoped<JsInterop>();//scoped for maui
        builder.Services.AddSingleton<FileHelper>();
        builder.Services.AddSingleton<PitstopsData>();
        builder.Services.AddTransient<DaysData>();
        builder.Services.AddTransient<Calc>();

        return builder.Build();
    }
}