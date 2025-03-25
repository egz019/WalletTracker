using WalletTracker.Common.Constants;

namespace WalletTracker.Managers;

public class AppContentManager : IAppContentManager
{
    private readonly IPreferenceManager _preferenceManager;
    private readonly IPreDataManager _preDataManager;
    private readonly IWalletTransactionsManager _walletTransactionsManager;


    public AppContentManager(
        IPreferenceManager preferenceManager,
        IPreDataManager preDataManager,
        IWalletTransactionsManager walletTransactionsManager
        )
    {
        _preferenceManager = preferenceManager;
        _preDataManager = preDataManager;
        _walletTransactionsManager = walletTransactionsManager;

    }

    public void PreloadAppData()
    {
        var isDataPreLoaded = _preferenceManager.Get<bool>(PreferenceKeys.IsDataPreloaded, false);
        if (isDataPreLoaded)
        {
            _preDataManager.InitializeData();
            return;
        }

        _preDataManager.PreloadData();
        _preDataManager.InitializeData();

        LoadTestTransactions();

        _preferenceManager.Set(PreferenceKeys.IsDataPreloaded, true, PreferenceKeys.IsDataPreloaded);
    }

    private void LoadTestTransactions()
    {
        var transactions = new List<WalletTransactionsEntity>
       {
           new () { TransactionId = Guid.NewGuid().ToString(), BudgetType = "01", BudgetSubType = "BS01", Amount = 30500, Description = "Main Salary to Forecastiing and Planning", TransactionDate = DateTime.Now },
           new () {TransactionId = Guid.NewGuid().ToString() ,BudgetType = "01", BudgetSubType = "BS01", Amount = 7500, Description = "Side Hustle Salary", TransactionDate = DateTime.Now },
           new () {TransactionId = Guid.NewGuid().ToString() ,BudgetType = "02", BudgetSubType = "BS03", Amount = 5200, Description = "Electric bill", TransactionDate = DateTime.Now },
           new () {TransactionId = Guid.NewGuid().ToString() ,BudgetType = "02", BudgetSubType = "BS03", Amount = 8200, Description = "Apartment rent", TransactionDate = DateTime.Now },
           new () {TransactionId = Guid.NewGuid().ToString() ,BudgetType = "02", BudgetSubType = "BS05", Amount = 4000, Description = "Groceries", TransactionDate = DateTime.Now },
           new () {TransactionId = Guid.NewGuid().ToString() ,BudgetType = "02", BudgetSubType = "BS03", Amount = 1900, Description = "Gas", TransactionDate = DateTime.Now },
           new () { TransactionId = Guid.NewGuid().ToString(), BudgetType = "02", BudgetSubType = "BS07", Amount = 1000, Description = "Donation", TransactionDate = DateTime.Now }
       };

        _walletTransactionsManager.SaveWalletTransactions(transactions);
    }
}
