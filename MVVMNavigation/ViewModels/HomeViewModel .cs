using ILearnIt.Services.Contracts;
using ILearnIt.ViewModels.Base;
using MVVMNavigation;
using System.Windows.Input;

namespace ILearnIt.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string title = string.Empty;
        protected readonly INavigationService NavigationService;

        public string Title
        {
            get => title;
            set
            {
                if (title == value) return;
                title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public ICommand NavigateBackCommand => new Command(() => NavigateBack());

        public HomeViewModel()
        {
            Title = "Hello, ";
            NavigationService = MauiProgram.ServiceProvider.GetRequiredService<INavigationService>();
        }

        public override async Task Init(object navigationData)
        {
            if (navigationData != null)
            {
                string pageName = navigationData.ToString();
                Title = string.Concat(Title, $" {pageName}");
            }
        }

        private async void NavigateBack()
        {
            await NavigationService.NavigateBackAsync("Reverse Navigation data");
        }
    }
}