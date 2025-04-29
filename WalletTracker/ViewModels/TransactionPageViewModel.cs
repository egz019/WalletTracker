
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

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

        ShowCurrentMonthTransactions = true;
    }

    protected override async Task InitializeAsync(INavigationParameters parameters)
    {
        await base.InitializeAsync(parameters);

        await RefreshWalletTransactions();
    }

    private async Task RefreshWalletTransactions()
    {
        var list = await _walletTransactionsManager.GetListOfWalletTransactionsAsync();
        WalletTransactionList = [ .. list
        .Where(_ => ShowCurrentMonthTransactions ? _.TransactionDate.Month == DateTime.Now.Month : _.TransactionDate.Year == DateTime.Now.Year)
        .Select(x => new WalletItemTransactionModel
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

    [ObservableProperty]
    private bool _showCurrentMonthTransactions;

    partial void OnShowCurrentMonthTransactionsChanged(bool oldValue, bool newValue)
    {
        if(oldValue != newValue)
        {
            MainThread.BeginInvokeOnMainThread(async () => { await RefreshWalletTransactions(); });     
        }
    }

    [RelayCommand]
    private async Task AddNewTransaction()
    {
        await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{ViewNames.AddNewTransactionPage}", 
        new NavigationParameters { { KnownNavigationParameters.UseModalNavigation, true }, {KnownNavigationParameters.Animated, true} });
    }

    [RelayCommand]
    private async Task DeleteTransaction(WalletItemTransactionModel transaction)
    {
        if (transaction == null)
        {
            return;
        }

        var response = await App.Current.Windows[0].Page.DisplayAlert("Delete Transaction", "Are you sure you want to delete this transaction?", "Yes", "No");
        if (!response)
        {
            return;
        }

        var isSuccess = await _walletTransactionsManager.DeleteWalletTransactionAsync(transaction.TransactionId);
        if (isSuccess)
        {
            var toast = Toast.Make("Transaction deleted.", ToastDuration.Short);
            await toast.Show();
            await RefreshWalletTransactions();
        }
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