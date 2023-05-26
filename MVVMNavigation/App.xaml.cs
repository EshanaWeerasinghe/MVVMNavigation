using ILearnIt.Services.Contracts;

namespace MVVMNavigation;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        InitNavigation();
    }

    private Task InitNavigation()
    {
        var navigationService = MauiProgram.ServiceProvider.GetRequiredService<INavigationService>();
        return navigationService.InitializeAsync();
    }
}