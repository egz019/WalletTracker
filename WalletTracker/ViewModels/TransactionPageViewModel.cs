

using System.Threading.Tasks;
using WalletTracker.Common;

namespace WalletTracker.ViewModels;

public partial class TransactionPageViewModel : PageViewModelBase
{
    private readonly IWalletTransactionsManager _walletTransactionsManager;
    public TransactionPageViewModel(BaseServices baseServices, IWalletTransactionsManager walletTransactionsManager) : base(baseServices)
    {
        _walletTransactionsManager = walletTransactionsManager;
    }

    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

        // if(parameters is List<WalletItemTransactionModel> walletTransactions)
        // {
        //     WalletTransactionList = walletTransactions;
        // }
        if(parameters.TryGetValue<List<WalletItemTransactionModel>>(NavigationParameterKeys.WalletTransactions, out var transactions))
        {
            WalletTransactionList = transactions;
        }
    }

    [ObservableProperty]
    private List<WalletItemTransactionModel> _walletTransactionList;
    
}