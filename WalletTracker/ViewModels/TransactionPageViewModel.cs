
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using WalletTracker.Extensions;

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
        .Where(_ => ShowCurrentMonthTransactions ? _.TransactionDate.Month == DateTime.Now.Month && _.TransactionDate.Year == DateTime.Now.Year 
                : _.TransactionDate.Year == DateTime.Now.Year)
        .Select(x => x.FromEntityToModel(_preDataManager))];
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
            if(_searchText != string.Empty)
            {
                return;
            }else
            {
                MainThread.BeginInvokeOnMainThread(async () => { await RefreshWalletTransactions(); });
            }
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
    private async Task EditTransaction(WalletItemTransactionModel transaction)
    {
        if (transaction == null)
        {
            return;
        }

        var navigationParams = new NavigationParameters
        {
            { KnownNavigationParameters.UseModalNavigation, true },
            { KnownNavigationParameters.Animated, true },
            { "Transaction", transaction }
        };

        await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{ViewNames.AddNewTransactionPage}", navigationParams);
    }


    [RelayCommand]
    private async Task Search()
    {
        if(string.IsNullOrEmpty(SearchText) || WalletTransactionList == null)
        {
            return;
        }

        var fullList = await _walletTransactionsManager.GetListOfWalletTransactionsAsync();
        
        WalletTransactionList = [ .. fullList
        .Select(x => x.FromEntityToModel(_preDataManager))
        .Where(x => ShowCurrentMonthTransactions ? x.TransactionDate.Month == DateTime.Now.Month && x.TransactionDate.Year == DateTime.Now.Year 
                : x.TransactionDate.Year == DateTime.Now.Year
                || x.TransactionDate.Date.ToShortDateString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
                || x.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
                || x.Amount.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
                || x.BudgetType.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) 
                || x.BudgetSubType.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase))
        ];
    }
}