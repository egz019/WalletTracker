using WalletTracker.Extensions;

namespace WalletTracker.ViewModels;

public partial class HomePageViewModel : PageViewModelBase
{
    private readonly IWalletTransactionsManager _walletTransactionManager;
    private readonly IPreDataManager _preDataManager;
    private List<BudgetSubTypeEntity> _budgetSubTypes;
    private List<BudgetTypeEntity> _budgetTypes;

    private List<WalletTransactionsEntity> _transactions;

    public HomePageViewModel(BaseServices baseServices, 
                            IWalletTransactionsManager walletTransactionManager, 
                            IPreDataManager preDataManager) : base(baseServices)
    {
        _walletTransactionManager = walletTransactionManager;
        _preDataManager = preDataManager;

        _walletTransactionManager.WalletTransactionListChanged += async (s,e) =>
        {
            await RefreshWalletTransactions();
        };
    }

    protected override async Task InitializeAsync(INavigationParameters parameters)
    {
        await base.InitializeAsync(parameters);

        CurrentMonthText = DateTime.Now.ToString("MMMM").ToUpper() + " " + DateTime.Now.Year;

        _budgetTypes = _preDataManager.BudgetTypes.ToList();
        _budgetSubTypes =  _preDataManager.BudgetSubTypes.ToList();

        _transactions = await _walletTransactionManager.GetListOfWalletTransactionsAsync();

        WalletTransactionList = [.. _transactions
        .Where(x => x.TransactionDate.Month == DateTime.Now.Month && x.TransactionDate.Year == DateTime.Now.Year)
        .Select(x => x.FromEntityToModel(_preDataManager))];

        Top5WalletTransactionList = [.. WalletTransactionList.Take(5)];

        GetTotalAmountPerBudgetType();

        ChartFormatOptions = GetChartFormat();
        WalletChartData = GetChartData();

        IsBusy = false;
    }

    [ObservableProperty]
    private List<WalletItemTransactionModel> _top5WalletTransactionList;

    [ObservableProperty]
    private List<WalletItemTransactionModel> _walletTransactionList;

    [ObservableProperty]
    private string _totalAmountPerIncome;

    [ObservableProperty]
    private string _totalAmountPerExpense;

    [ObservableProperty]
    private string _currentMonthText;

    private async Task RefreshWalletTransactions()
    {
        _transactions = await _walletTransactionManager.GetListOfWalletTransactionsAsync();

        WalletTransactionList = [.. _transactions
        .Where(x => x.TransactionDate.Month == DateTime.Now.Month && x.TransactionDate.Year == DateTime.Now.Year)
        .Select(x => x.FromEntityToModel(_preDataManager))];

        GetTotalAmountPerBudgetType();

        WalletChartData = GetChartData();
    }

    private void GetTotalAmountPerBudgetType()
    {
        if (!WalletTransactionList.Any())
        {
            return;
        }

        var groupedItems = WalletTransactionList
        .GroupBy(x => x.BudgetType.Code)
        .Select(x => new 
        { 
            BudgetType = x.Key, 
            TotalAmount = x.Sum(y => y.Amount)
        });

        TotalAmountPerIncome = groupedItems.FirstOrDefault(_ => _.BudgetType == "01")?.TotalAmount.ToString("C");
        TotalAmountPerExpense = groupedItems.FirstOrDefault(_ => _.BudgetType == "02")?.TotalAmount.ToString("C");
    }

    [ObservableProperty]
    private object _walletChartData;

    private object GetChartData()
    {
        if (!WalletTransactionList.Any())
        {
            return new object();
        }

        var groupedItems = WalletTransactionList
        .GroupBy(x => _budgetSubTypes.FirstOrDefault(_ => _.Code == x.BudgetSubType.Code).Description)
        .Select(x => new
        { 
            Transaction = x.Key, 
            Amount = x.Sum(y => y.Amount)
        });

        var columnData = new List<object>()
        {
            new { id = "", label = "Budget Sub Type", pattern = "", type = "string" },
            new { id = "", label = "Total Amount", pattern = "", type = "number" }
        };

        var rowsData = new List<object>();

        foreach (var item in groupedItems)
        {
            rowsData.Add(new { c = new List<object> { new { v = item.Transaction }, new { v = item.Amount } } });
        }

        return new
        {
            cols = columnData,
            rows = rowsData
        };
    }

    [ObservableProperty]
    private object _chartFormatOptions;

    public bool IsBusy { get; private set; }

    private object GetChartFormat() => new
    {
        fontSize = DeviceInfo.Current.Platform == DevicePlatform.Android ? "10" : "12",
        fontName = "Raleway-Regular",
        chartArea = new
        {
            width = DeviceInfo.Current.Platform == DevicePlatform.Android ? "90%" : "35%",
            height = DeviceInfo.Current.Platform == DevicePlatform.Android ? "70%" : "85%",
        },
        colors = new[] { "#87BB62", "#4394E5", "#B6A6E9", "#F8AE54", "#CA6C0F", "#003366", "#21134D" },
        legend = new
        {
            position = "right",
            textStyle = new { fontSize = 10 },
        },
        tooltip = new
        {
            isHtml = true,
            showColorCode = true
        },
        animation = new
        {
            duration = 2000,
            easing = "out",
            startup = true
        },
        pieHole = 0.35,
    };

    [RelayCommand]
    private async Task ViewMore()
    {
        var navigationParams = new NavigationParameters
        {
            { NavigationParameterKeys.WalletTransactions, WalletTransactionList.ToList() }
        };

        await NavigationService.SelectTabAsync(ViewNames.TransactionPage, navigationParams);
    }
}
