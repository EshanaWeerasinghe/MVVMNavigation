using System.Windows.Input;
using ILearnIt.Services.Contracts;
using ILearnIt.ViewModels.Base;
using MVVMNavigation;

namespace ILearnIt.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int count = 0;
        private string counterLabel;
        protected readonly INavigationService NavigationService;

        public string CounterLabel
        {
            get => counterLabel;
            set
            {
                if (counterLabel == value) return;
                counterLabel = value;
                RaisePropertyChanged(() => CounterLabel);
            }
        }

        public ICommand NavigateToHomeViewCommand => new Command(() => NavigateToHomePage());
        public ICommand IncreaseCounterCommand => new Command(() => IncreaseCounter());

        public MainViewModel()
        {
            CounterLabel = "Current count: 0";
            NavigationService = MauiProgram.ServiceProvider.GetRequiredService<INavigationService>();
        }

        private async void NavigateToHomePage()
        {
            await NavigationService.NavigateToAsync<HomeViewModel>("Welcome to Home view");
        }

        private void IncreaseCounter()
        {
            count++;
            CounterLabel = $"Current count: {count}";
        }

        public override async Task ReverseInit(object navigationData)
        {
            await Task.FromResult(false);
        }
    }
}