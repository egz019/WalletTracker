
namespace WalletTracker.ViewModels;

public partial class HomePageViewModel : PageViewModelBase
{
    public HomePageViewModel(BaseServices baseServices) : base(baseServices)
    {
    }

    protected override Task InitializeAsync(INavigationParameters parameters)
    {
        base.InitializeAsync(parameters);

        CurrentMonthText = DateTime.Now.ToString("MMMM").ToUpper() + " " + DateTime.Now.Year;

        GetTotalAmountPerBudgetType();

        return Task.CompletedTask;
    }

    private readonly ObservableCollection<BudgetItemAccountModel> _budgetItems =
    [
        new (){Id = 1, BudgetType = BudgetType.Income, BudgetSubTypeModel = new BudgetSubTypeModel(BudgetSubType.Salary), Amount = 30500, Description = "Main Salary to Forecastiing and Planning"},
        new (){Id = 2, BudgetType = BudgetType.Income, BudgetSubTypeModel = new BudgetSubTypeModel(BudgetSubType.Salary), Amount = 7500, Description = "Side Hustle Salary"},
        new (){Id = 3, BudgetType = BudgetType.Expense, BudgetSubTypeModel = new BudgetSubTypeModel(BudgetSubType.Utilities), Amount = 5200, Description = "Electric bill"},
        new (){Id = 4, BudgetType = BudgetType.Expense, BudgetSubTypeModel = new BudgetSubTypeModel(BudgetSubType.Utilities), Amount = 8200, Description = "Apartment rent"},
        new (){Id = 5, BudgetType = BudgetType.Expense, BudgetSubTypeModel = new BudgetSubTypeModel(BudgetSubType.Food), Amount = 4000, Description = "Groceries"},
        new (){Id = 6, BudgetType = BudgetType.Expense, BudgetSubTypeModel = new BudgetSubTypeModel(BudgetSubType.Utilities), Amount = 1900, Description = "Gas"},
        new (){Id = 7, BudgetType = BudgetType.Expense, BudgetSubTypeModel = new BudgetSubTypeModel(BudgetSubType.Miscellaneous), Amount = 1000, Description = "Donation"},
    ];

    public Chart WalletChart => GetChart();

    private DonutChart GetChart()
    {
        var chartEntries = new List<ChartEntry>();

        if (BudgetList.Count <= 0)
        {
            return new DonutChart();
        }

        var groupedItems = BudgetList
                .GroupBy(x => x.BudgetSubType.ToString())
                .Select(x => new
                {
                    Transaction = x.Key,
                    Amount = (float?)x.Sum(y => y.Amount)
                });

        foreach (var item in groupedItems)
        {
            chartEntries.Add(new Microcharts.ChartEntry(item.Amount)
            {
                Label = item.Transaction,
                ValueLabel = item.Amount.ToString(),
                Color = GetBudgetSubTypeLegendColor(item.Transaction)
            });
        }

        return new DonutChart
        {
            Entries = chartEntries
        };
    }

    private void GetTotalAmountPerBudgetType()
    {
        if (BudgetList.Count <= 0)
        {
            return;
        }

        var groupedItems = BudgetList
               .GroupBy(x => x.BudgetType.ToString())
               .Select(x => new
               {
                   BudgetType = x.Key,
                   TotalAmount = (float?)x.Sum(y => y.Amount)
               });

        TotalAmountPerIncome = groupedItems.FirstOrDefault(_ => _.BudgetType == "Income")?.TotalAmount.ToString();
        TotalAmountPerExpense = groupedItems.FirstOrDefault(_ => _.BudgetType == "Expense")?.TotalAmount.ToString();
    }
    
    public object WalletChartData => GetChartData();

    private object GetChartData()
    {
        //if (BudgetList.Count <= 0)
        //{
        //    return new object;
        //}

        var groupedItems = BudgetList
                .GroupBy(x => x.BudgetSubType.ToString())
                .Select(x => new
                {
                    Transaction = x.Key,
                    Amount = (float?)x.Sum(y => y.Amount)
                });

        var columnData = new List<object>()
            {
                new { id = "", label = "Budget Type", pattern = "", type = "string" },
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

    public static object ChartFormatOptions => new
    {
        fontSize = 10,
        fontName = "Raleway-Regular",
        chartArea = new
        {
            width = "90%",
            height = "70%"
        },
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
            duration = 1000,
            easing = "out",
            startup = true
        },
        pieHole = 0.35,
    };

    private SKColor GetBudgetSubTypeLegendColor(string budgetSubType)
    {
        return budgetSubType switch
        {
            "Salary" => SKColor.Parse("#77d065"),
            "Utilities" => SKColor.Parse("#f14668"),
            "Rent" => SKColor.Parse("#3d85c6"),
            "Transportation" => SKColor.Parse("#846ec5"),
            "Food" => SKColor.Parse("#2c48df"),
            "Shopping" => SKColor.Parse("#f4bd18"),
            "Miscellaneous" => SKColor.Parse("#ef8e29"),
            _ => SKColor.Parse("#f14668"),
        };
    }

    public ObservableCollection<BudgetItemAccountModel> BudgetList => _budgetItems;

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

    [ObservableProperty]
    private string _totalAmountPerIncome;

    [ObservableProperty]
    private string _totalAmountPerExpense;

    [ObservableProperty]
    private string _currentMonthText;

    [RelayCommand(AllowConcurrentExecutions = false)]
    private async Task TryMeTapped()
    {
        CurrentMonthText = "Month";

        SemanticScreenReader.Announce(CurrentMonthText);
    }

}
