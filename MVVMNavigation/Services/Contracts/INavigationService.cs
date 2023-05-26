using ILearnIt.ViewModels.Base;

namespace ILearnIt.Services.Contracts
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToAsync(Type viewModelType);

        Task NavigateToAsync(Type viewModelType, object parameter);

        Task NavigateBackAsync();

        Task NavigateBackAsync(object parameter);

        Task RemoveLastFromBackStackAsync();
    }
}