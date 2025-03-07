

namespace WalletTracker.ViewModels;
    public partial class PageViewModelBase : ObservableObject, IPageLifecycleAware, IInitialize, INavigationAware, IDestructible
    {
        private readonly BaseServices _baseServices;

        protected PageViewModelBase(BaseServices baseServices)
        {
            _baseServices = baseServices;
        }

        public INavigationService NavigationService => _baseServices.NavigationService;
        public IPageDialogService PageDialogService => _baseServices.PageDialogService;
        public IEventAggregator EventAggregator => _baseServices.EventAggregator;

        protected virtual void OnAppearing()
        {
        }

        protected virtual void OnDisappearing()
        {
        }

        protected virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNavigatedFromAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        protected virtual void Destroy()
        {
        }

        void IPageLifecycleAware.OnAppearing()
        {
            OnAppearing();
        }

        void IPageLifecycleAware.OnDisappearing()
        {
            OnDisappearing();
        }

        async void IInitialize.Initialize(INavigationParameters parameters)
        {
            await InitializeAsync(parameters);
        }

        async void INavigatedAware.OnNavigatedFrom(INavigationParameters parameters)
        {
            await OnNavigatedFromAsync(parameters);
        }

        async void INavigatedAware.OnNavigatedTo(INavigationParameters parameters)
        {
            await OnNavigatedToAsync(parameters);
        }

        void IDestructible.Destroy()
        {
            Destroy();
        }
    }
