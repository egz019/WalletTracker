using Mapster;
using WalletTracker.Repositories;
using WalletTracker.Repositories.Interfaces;

namespace WalletTracker;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterModules)
            .ConfigureModuleCatalog(ConfigureModuleCatalog)
            .OnInitialized(OnInitialized)
            .CreateWindow(async (containerRegistry, navigationService) =>
            {
                await InitAsync(containerRegistry, navigationService);
            });
    }

    private async static Task InitAsync(IContainerProvider containerProvider, INavigationService navigationService)
    {
        // var appDb = containerProvider.Resolve<IAppDatabase>();
        // await appDb.InitializeTables();
        //await System.Threading.Tasks.Task.Delay(10000);
        //Pre-loaded data here
        var appContentManager = containerProvider.Resolve<IAppContentManager>();
        appContentManager.PreloadAppData();

        await navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
    }
    
    private static void RegisterModules(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterRepositories();
        containerRegistry.RegisterManagers();
        containerRegistry.RegisterViews();
        containerRegistry.RegisterServices();
    }

    private static void RegisterViews(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
        containerRegistry.RegisterForNavigation<TransactionPage, TransactionPageViewModel>();
        containerRegistry.RegisterForNavigation<AddNewTransactionPage, AddNewTransactionPageViewModel>();
        containerRegistry.RegisterForNavigation<ReportsPage>();
    }

    private static void RegisterRepositories(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IAppDatabase, AppDatabase>();
        //containerRegistry.RegisterTypes(typeof(IRepository));
        containerRegistry.RegisterSingleton<IMobileDatabase, MobileDatabase>();
        containerRegistry.RegisterSingleton<IBudgetTypeRepository, BudgetTypeRepository>();
        containerRegistry.RegisterSingleton<IBudgetSubTypeRepository, BudgetSubTypeRepository>();
        containerRegistry.RegisterSingleton<IPreferenceRespository, PreferenceRepository>();
        containerRegistry.RegisterSingleton<IWalletTransactionsRepository, WalletTransactionsRepository>();
    }

    private static void RegisterManagers(this IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterInstance(TypeAdapterConfig.GlobalSettings);
        containerRegistry.RegisterScoped<IMapper, MapsterMapper.ServiceMapper>();
        containerRegistry.RegisterSingleton<IServiceMapper, WalletTracker.Managers.ServiceMapper.ServiceMapper>();
        //containerRegistry.RegisterTypes(typeof(IManager));
        containerRegistry.RegisterSingleton<IManagerToolkit, ManagerToolkit>();
        containerRegistry.RegisterSingleton<IAppContentManager, AppContentManager>();
        containerRegistry.RegisterSingleton<IPreDataManager, PreDataManager>();
        containerRegistry.RegisterSingleton<IPreferenceManager, PreferenceManager>();
        containerRegistry.RegisterSingleton<IWalletTransactionsManager, WalletTransactionsManager>();
    }

    private static void RegisterServices(this IContainerRegistry containerRegistry)
    {

    }

    private static void OnInitialized(IContainerProvider container)
    {
        
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
