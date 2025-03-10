namespace WalletTracker;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
            .ConfigureModuleCatalog(ConfigureModuleCatalog)
            .OnInitialized(OnInitialized)
            .CreateWindow(async (containerRegistry, navigationService) =>
            {
                await navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
            });
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterViews();
        containerRegistry.RegisterServices();
        containerRegistry.RegisterRepositories();
        containerRegistry.RegisterManagers();
    }

    private static void RegisterViews(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
        containerRegistry.RegisterForNavigation<TransactionPage>();
        containerRegistry.RegisterForNavigation<ReportsPage>();
    }

    private static void RegisterRepositories(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IAppDatabase, AppDatabase>();
        containerRegistry.RegisterTypes(typeof(IRepository));
    }

    private static void RegisterManagers(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterScoped<IMapper, MapsterMapper.ServiceMapper>();
        containerRegistry.RegisterSingleton<IServiceMapper, WalletTracker.Managers.ServiceMapper.ServiceMapper>();
        containerRegistry.RegisterTypes(typeof(IManager));
    }

    private static void RegisterServices(this IContainerRegistry containerRegistry)
    {

    }

    private static void OnInitialized(IContainerProvider container)
    {
        //Save Pre-loaded data here
        
    }

    private static void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {

    }

    private static void RegisterTypes(this IContainerRegistry containerRegistry, Type typeInterface)
    {
        var types = typeInterface
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

        foreach (var type in types)
            if (typeInterface.IsAssignableFrom(type.Service))
                containerRegistry.RegisterSingleton(type.Service, type.Implementation);
    }
}
