
using System.Windows.Input;
using WalletTracker.Common;
using WalletTracker.Entities;
using WalletTracker.Managers;

namespace WalletTracker.ViewModels;

public partial class HomePageViewModel : PageViewModelBase
{
    private readonly IWalletTransactionsManager _walletTransactionManager;
    private readonly IPreDataManager _preDataManager;

    private List<BudgetSubTypeEntity> _budgetSubTypes;
    private List<BudgetTypeEntity> _budgetTypes;

    private List<WalletTransactionsEntity> _transactions;

    public HomePageViewModel(BaseServices baseServices, IWalletTransactionsManager walletTransactionManager, IPreDataManager preDataManager) : base(baseServices)
    {
        _walletTransactionManager = walletTransactionManager;
        _preDataManager = preDataManager;
    }

    protected override async Task InitializeAsync(INavigationParameters parameters)
    {
        await base.InitializeAsync(parameters);

        CurrentMonthText = DateTime.Now.ToString("MMMM").ToUpper() + " " + DateTime.Now.Year;

        _budgetTypes = await _preDataManager.GetBudgetTypeList();
        _budgetSubTypes =  await _preDataManager.GetBudgetSubTypeList();

        _transactions = await _walletTransactionManager.GetListOfWalletTransactionsAsync();
        WalletTransactionList = _transactions.Select(x => new WalletItemTransactionModel
        {
            TransactionId = x.TransactionId,
            BudgetType = new BudgetTypeModel()
            { 
                Code = x.BudgetType, 
                Description = _budgetTypes.FirstOrDefault(_ => _.Code == x.BudgetType).Description,
                IsAdd = _budgetTypes.FirstOrDefault(_ => _.Code == x.BudgetType).IsAdd
            },
            BudgetSubType = new BudgetSubTypeModel()
            { 
                Code = x.BudgetSubType, 
                Description = _budgetSubTypes.FirstOrDefault(_ => _.Code == x.BudgetSubType).Description,
                Icon = _budgetSubTypes.FirstOrDefault(_ => _.Code == x.BudgetSubType).Icon,
            },
            Amount = x.Amount,
            Description = x.Description,
            TransactionDate = x.TransactionDate,
        }).ToList();

        Top5WalletTransactionList = [.. WalletTransactionList.Take(5)];

        GetTotalAmountPerBudgetType();

        ChartFormatOptions = GetChartFormat();
        WalletChartData = GetChartData();

         IsBusy = false;

       
        //return Task.CompletedTask;
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

    private void GetTotalAmountPerBudgetType()
    {
        if (!WalletTransactionList.Any())
        {
            return;
        }

        var groupedItems = WalletTransactionList
        .GroupBy(x => x.BudgetType)
        .Select(x => new 
        { 
            BudgetType = x.Key, 
            TotalAmount = x.Sum(y => y.Amount)
        });

        TotalAmountPerIncome = groupedItems.FirstOrDefault(_ => _.BudgetType.Code == "01")?.TotalAmount.ToString("C");
        TotalAmountPerExpense = groupedItems.FirstOrDefault(_ => _.BudgetType.Code == "02")?.TotalAmount.ToString("C");
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

    private object GetChartFormat()
     => new
    {
        fontSize = 10,
        fontName = "Raleway-Regular",
        chartArea = new
        {
            width = "90%",
            height = "70%"
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
            { NavigationParameterKeys.WalletTransactions, _walletTransactionList.ToList() }
        };
        await NavigationService.SelectTabAsync(ViewNames.TransactionPage, navigationParams);//, new Uri($"TabbedPage\\selectedTab={ViewNames.TransactionPage}"));

    }

    //public Chart WalletChart => GetChart();

    //private DonutChart GetChart()
    //{
    //    var chartEntries = new List<ChartEntry>();

    //    if (BudgetList.Count <= 0)
    //    {
    //        return new DonutChart();
    //    }

    //    var groupedItems = BudgetList
    //            .GroupBy(x => x.BudgetSubType.ToString())
    //            .Select(x => new
    //            {
    //                Transaction = x.Key,
    //                Amount = (float?)x.Sum(y => y.Amount)
    //            });

    //    foreach (var item in groupedItems)
    //    {
    //        chartEntries.Add(new Microcharts.ChartEntry(item.Amount)
    //        {
    //            Label = item.Transaction,
    //            ValueLabel = item.Amount.ToString(),
    //            Color = GetBudgetSubTypeLegendColor(item.Transaction)
    //        });
    //    }

    //    return new DonutChart
    //    {
    //        Entries = chartEntries
    //    };
    //}



    //public object WalletChartData => GetChartData();

    //private object GetChartData()
    //{
    //    //if (BudgetList.Count <= 0)
    //    //{
    //    //    return new object;
    //    //}

    //    var groupedItems = BudgetList
    //            .GroupBy(x => _budgetSubTypes.FirstOrDefault(_ => _.Code == x.BudgetSubType).Description)
    //            .Select(x => new
    //            {
    //                Transaction = x.Key,
    //                Amount = (float?)x.Sum(y => y.Amount)
    //            });

    //    var columnData = new List<object>()
    //        {
    //            new { id = "", label = "Budget Sub Type", pattern = "", type = "string" },
    //            new { id = "", label = "Total Amount", pattern = "", type = "number" }
    //        };

    //    var rowsData = new List<object>();

    //    foreach (var item in groupedItems)
    //    {
    //        rowsData.Add(new { c = new List<object> { new { v = item.Transaction }, new { v = item.Amount } } });
    //    }

    //    return new
    //    {
    //        cols = columnData,
    //        rows = rowsData
    //    };
    //}

    //public static object ChartFormatOptions => new
    //{
    //    fontSize = 10,
    //    fontName = "Raleway-Regular",
    //    chartArea = new
    //    {
    //        width = "90%",
    //        height = "70%"
    //    },
    //    legend = new
    //    {
    //        position = "right",
    //        textStyle = new { fontSize = 10 },

    //    },
    //    tooltip = new
    //    {
    //        isHtml = true,
    //        showColorCode = true
    //    },
    //    animation = new
    //    {
    //        duration = 1000,
    //        easing = "out",
    //        startup = true
    //    },
    //    pieHole = 0.35,
    //};

    //private SKColor GetBudgetSubTypeLegendColor(string budgetSubType)
    //{
    //    return budgetSubType switch
    //    {
    //        "Salary" => SKColor.Parse("#77d065"),
    //        "Utilities" => SKColor.Parse("#f14668"),
    //        "Rent" => SKColor.Parse("#3d85c6"),
    //        "Transportation" => SKColor.Parse("#846ec5"),
    //        "Food" => SKColor.Parse("#2c48df"),
    //        "Shopping" => SKColor.Parse("#f4bd18"),
    //        "Miscellaneous" => SKColor.Parse("#ef8e29"),
    //        _ => SKColor.Parse("#f14668"),
    //    };
    //}



    //private List<BudgetItemAccountModel> GetBudgetList()
    //{
    //    _budgetItems.AddRange(new BudgetItemAccountModel[] {
    //        new BudgetItemAccountModel{Id = 1, BudgetType = BudgetType.Income, BudgetSubType = BudgetSubType.Salary, Amount = 11500, Description = "Main Salary"},
    //        new BudgetItemAccountModel{Id = 2, BudgetType = BudgetType.Income, BudgetSubType = BudgetSubType.Salary, Amount = 1500, Description = "Side Hustle Salary"},
    //        new BudgetItemAccountModel{Id = 3, BudgetType = BudgetType.Expense, BudgetSubType = BudgetSubType.Utilities, Amount = 200, Description = "Electric bill"},
    //        new BudgetItemAccountModel{Id = 4, BudgetType = BudgetType.Expense, BudgetSubType = BudgetSubType.Rent, Amount = 1200, Description = "Apartment rent"},
    //        new BudgetItemAccountModel{Id = 5, BudgetType = BudgetType.Expense, BudgetSubType = BudgetSubType.Food, Amount = 800, Description = "Groceries"},
    //        new BudgetItemAccountModel{Id = 6, BudgetType = BudgetType.Expense, BudgetSubType = BudgetSubType.Utilities, Amount = 400, Description = "Gas"},
    //        new BudgetItemAccountModel{Id = 7, BudgetType = BudgetType.Expense, BudgetSubType = BudgetSubType.Miscellaneous, Amount = 100, Description = "Donation"},
    //    });

    //    return _budgetItems;
    //}

   
   

    //[RelayCommand(AllowConcurrentExecutions = false)]
    //private async Task TryMeTapped()
    //{
    //    CurrentMonthText = "Month";

    //    SemanticScreenReader.Announce(CurrentMonthText);
    //}

}
