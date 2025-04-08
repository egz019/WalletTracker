namespace WalletTracker.ViewModels;

public partial class TransactionPageViewModel : PageViewModelBase
{
    private readonly IWalletTransactionsManager _walletTransactionsManager;
    private readonly IPreDataManager _preDataManager;

    public TransactionPageViewModel(
        BaseServices baseServices, 
    IWalletTransactionsManager walletTransactionsManager, 
    IPreDataManager preDataManager) : base(baseServices)
    {
        _walletTransactionsManager = walletTransactionsManager;
        _preDataManager = preDataManager;

        _walletTransactionsManager.WalletTransactionListChanged += async (s, e) =>
        {
            await RefreshWalletTransactions();
        };  
    }

    private async Task RefreshWalletTransactions()
    {
        var list = await _walletTransactionsManager.GetListOfWalletTransactionsAsync();
        WalletTransactionList = [ .. list.Select(x => new WalletItemTransactionModel
        {
            TransactionId = x.TransactionId,
            BudgetType = new BudgetTypeModel()
            {
                Code = x.BudgetType,
                Description = _preDataManager.BudgetTypes.FirstOrDefault(_ => _.Code == x.BudgetType).Description,
                IsAdd = _preDataManager.BudgetTypes.FirstOrDefault(_ => _.Code == x.BudgetType).IsAdd
            },
            BudgetSubType = new BudgetSubTypeModel()
            {
                Code = x.BudgetSubType,
                Description = _preDataManager.BudgetSubTypes.FirstOrDefault(_ => _.Code == x.BudgetSubType).Description,
                Icon = _preDataManager.BudgetSubTypes.FirstOrDefault(_ => _.Code == x.BudgetSubType).Icon,
            },
            Amount = x.Amount,
            Description = x.Description,
            TransactionDate = x.TransactionDate,
        })];
    }

    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

        // if(parameters.TryGetValue<List<WalletItemTransactionModel>>(NavigationParameterKeys.WalletTransactions, out var transactions))
        // {
        //     WalletTransactionList = transactions;
        // }
        
        await RefreshWalletTransactions();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    [ObservableProperty]
    private string _searchText;

    partial void OnSearchTextChanged(string oldValue, string newValue)
    {
        if (newValue == string.Empty && oldValue != string.Empty)
        {
            MainThread.BeginInvokeOnMainThread(async () => { await RefreshWalletTransactions(); }); 
        }
    }

    [ObservableProperty]
    private List<WalletItemTransactionModel> _walletTransactionList;

    [RelayCommand]
    private async Task AddNewTransaction()
    {
        await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{ViewNames.AddNewTransactionPage}", 
        new NavigationParameters { { KnownNavigationParameters.UseModalNavigation, true }, {KnownNavigationParameters.Animated, true} });
    }

    [RelayCommand]
    private void Search()
    {
        if(string.IsNullOrEmpty(SearchText) || WalletTransactionList == null)
        {
            return;
        }

        WalletTransactionList = [.. WalletTransactionList
        .Where(x => x.TransactionDate.Date.ToShortDateString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
        || x.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
        || x.Amount.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
        || x.BudgetType.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
        || x.BudgetSubType.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase))];
    }
}