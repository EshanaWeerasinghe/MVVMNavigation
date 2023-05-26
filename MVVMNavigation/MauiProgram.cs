using ILearnIt.Services.Contracts;
using ILearnIt.Services.Navigation;
using ILearnIt.ViewModels;
using Microsoft.Extensions.Logging;

namespace MVVMNavigation;

public static class MauiProgram
{
    public static IServiceProvider ServiceProvider;
    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        //Services
        builder.Services.AddSingleton<INavigationService, NavigationService>();

        //ViewModels
        builder.Services.AddScoped<MainViewModel>();
        builder.Services.AddScoped<HomeViewModel>();

        var host = builder.Build();
        ServiceProvider = host.Services;
        return host;
    }
}
