using ILearnIt.Services.Contracts;
using ILearnIt.ViewModels;
using ILearnIt.ViewModels.Base;

namespace ILearnIt.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainViewModel>();
        }

        //NavigateBack
        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainView)
            {
                return;
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public async Task NavigateBackAsync(object parameter)
        {
            var nav = CurrentApplication.MainPage.Navigation;

            if (nav != null)
            {
                nav.RemovePage(nav.NavigationStack[nav.NavigationStack.Count - 1]);

                var page = nav.NavigationStack.LastOrDefault();
                if (page != null)
                {
                    await (page.BindingContext as ViewModelBase).ReverseInit(parameter);
                }
            }
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as MainView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        //NavigateTo
        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is MainView)
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage.Navigation;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = page;
                }
            }
            await (page.BindingContext as ViewModelBase).Init(parameter);

            //Navigation Bar Color Change
            //var navigationPage1 = CurrentApplication.MainPage as NavigationPage;
            //navigationPage1.BarBackgroundColor = Color.FromArgb("#ffcd45");
            //navigationPage1.BarTextColor = Color.FromArgb("#f70c28");
        }

        private Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase vm = Activator.CreateInstance(viewModelType) as ViewModelBase;

            page.BindingContext = vm;
            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }
            return _mappings[viewModelType];
        }

        //Page To ViewModel Mappings
        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(MainViewModel), typeof(MainView));
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
        }
    }
}