namespace WalletTracker.Services;

public class BaseServices
{
    public BaseServices(INavigationService navigationService,
        IPageDialogService pageDialogService,
        IEventAggregator eventAggregator,
        IRegionManager regionManager)
    {
        NavigationService = navigationService;
        PageDialogService = pageDialogService;
        EventAggregator = eventAggregator;
        RegionManager = regionManager;
    }

    public INavigationService NavigationService { get; }

    public IPageDialogService PageDialogService { get; }

    public IEventAggregator EventAggregator { get; }

    public IRegionManager RegionManager { get; set; }
}
