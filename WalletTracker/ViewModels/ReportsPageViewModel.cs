using System.Globalization;
using System.Text;

namespace WalletTracker.ViewModels;

public partial class ReportsPageViewModel : PageViewModelBase
{
    private readonly IPreDataManager _preDataManager;
    private readonly IWalletTransactionsManager _walletTransactionsManager;
    private IList<BudgetTypeEntity> _budgetTypes;
    private IList<BudgetSubTypeEntity> _budgetSubTypes;

    protected ReportsPageViewModel(
        BaseServices baseServices,
        IPreDataManager preDataManager,
        IWalletTransactionsManager walletTransactionsManager) : base(baseServices)
    {
        _preDataManager = preDataManager;
        _walletTransactionsManager = walletTransactionsManager;
    }

    protected override Task InitializeAsync(INavigationParameters parameters)
    {
        base.InitializeAsync(parameters);

        _budgetTypes = _preDataManager.BudgetTypes;

        _budgetSubTypes = _preDataManager.BudgetSubTypes;

        MonthFilter = DateTime.Now;
        YearFilter = DateTime.Now;
        IsMonthly = true;

        return Task.CompletedTask;
    }

    [ObservableProperty]
    private bool _isMonthly;

    [ObservableProperty]
    private bool _isAnnual;

    [ObservableProperty]
    private DateTime _monthFilter;

    [ObservableProperty]
    private DateTime _yearFilter;

    [ObservableProperty]
    private bool _excludeTransfers;

    [ObservableProperty]
    private object _mainReportData;

    [ObservableProperty]
    private object _incomeVsExpenseReportData;

    [ObservableProperty]
    private bool _hasReportData;

    [ObservableProperty]
    private string _mainReportTitle;

    private object GetMainReportData(List<WalletItemTransactionModel> walletData)
    {
        if (!walletData.Any())
        {
            return new object();
        }

        var groupedItems = walletData
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

    private object GetMonthlyIncomeVsExpenseReportData(List<WalletItemTransactionModel> walletData)
    {
        if (!walletData.Any())
        {
            return new object();
        }
        var columnData = new List<object>()
        {
            new { id = "", label = "Month", pattern = "", type = "string" },
        };

        foreach (var item in _budgetTypes)
        {
            columnData.Add(new { id = "", label = item.Description, pattern = "", type = "number" });
        }

        var baseData = walletData.Select(x => new
        {
            Month = x.TransactionDate.ToString("MMM", CultureInfo.InvariantCulture),
            BudgetType = _budgetTypes.FirstOrDefault(_ => _.Code == x.BudgetType.Code).Description,
            IncomeAmount = x.BudgetType.IsAdd ? x.Amount : 0,
            ExpenseAmount = x.BudgetType.IsAdd ? 0 : x.Amount
        });

        var groupedItems = baseData.GroupBy(x => new
        {
            Month = x.Month,
        })
        .Select(x => new
        {
            Transaction = x.Key,
            IncomeAmount = x.Sum(y => y.IncomeAmount),
            ExpenseAmount = x.Sum(y => y.ExpenseAmount),
        });

        var rowsData = new List<object>();
        foreach (var item in groupedItems)
        {
            rowsData.Add(new { c = new List<object> { new { v = item.Transaction.Month }, new { v = item.IncomeAmount }, new { v = item.ExpenseAmount } } });
        }

        return new
        {
            cols = columnData,
            rows = rowsData
        };
    }

    private object GetAnnualIncomeVsExpenseReportData(List<WalletItemTransactionModel> walletData)
    {
        if (!walletData.Any())
        {
            return new object();
        }
        var columnData = new List<object>()
        {
            new { id = "", label = "Month", pattern = "", type = "string" },
        };

        foreach (var item in _budgetTypes)
        {
            columnData.Add(new { id = "", label = item.Description, pattern = "", type = "number" });
        }

        var baseData = walletData.Select(x => new
        {
            Month = x.TransactionDate.ToString("MMM", CultureInfo.InvariantCulture),
            BudgetType = _budgetTypes.FirstOrDefault(_ => _.Code == x.BudgetType.Code).Description,
            IncomeAmount = x.BudgetType.IsAdd ? x.Amount : 0,
            ExpenseAmount = x.BudgetType.IsAdd ? 0 : x.Amount
        });

        var groupedItems = baseData.GroupBy(x => new
        {
            Month = x.Month,
        })
        .Select(x => new
        {
            Transaction = x.Key,
            IncomeAmount = x.Sum(y => y.IncomeAmount),
            ExpenseAmount = x.Sum(y => y.ExpenseAmount),
        });

        var rowsData = new List<object>();
        foreach (var item in groupedItems)
        {
            rowsData.Add(new { c = new List<object> { new { v = item.Transaction.Month }, new { v = item.IncomeAmount }, new { v = item.ExpenseAmount } } });
        }

        return new
        {
            cols = columnData,
            rows = rowsData
        };
    }

    [ObservableProperty]
    private object _mainFormatOptions;

    [ObservableProperty]
    private object _incomeVsExpenseFormatOptions;

    [ObservableProperty]
    private string _incomeVsExpenseTitle;

    private object InitializeMainReportFormat() => new
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

    private object InitializeIncomeVsExpenseReportFormat() => new
    {
        fontSize = 10,
        fontName = "Raleway-Regular",
        chartArea = new
        {
            width = "90%",
            height = "70%"
        },
        lineWidth = 3,
        pointSize = 5,
        hAxis = new
        {
            title = "Amount",
            minValue = 0,
            textStyle = new { fontSize = 10, color = "#333" },
            titleTextStyle = new { fontSize = 12, color = "#666" },
            gridlines = new { count = 6 }
        },
        vAxis = new
        {
            title = IsAnnual ? "Month" : "Budget Type",
            minValue = 1,
            textStyle = new { fontSize = 10, color = "#333" },
            titleTextStyle = new { fontSize = 12, color = "#666" }
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
    };

    [RelayCommand]
    private async Task GenerateReport()
    {
        var filteredTransactions = new List<WalletTransactionsEntity>();
        var finalData = new List<WalletItemTransactionModel>();
        var transactions = await _walletTransactionsManager.GetListOfWalletTransactionsAsync();

        if (transactions == null || transactions.Count == 0)
        {
            HasReportData = false;
            return;
        }

        //Execute report filters
        if (IsMonthly)
        {
            filteredTransactions = transactions.Where(t => t.TransactionDate.Month == MonthFilter.Month && t.TransactionDate.Year == MonthFilter.Year).ToList();
        }
        else if (IsAnnual)
        {
            filteredTransactions = transactions.Where(t => t.TransactionDate.Year == MonthFilter.Year).ToList();
        }

        if (ExcludeTransfers)
        {
            filteredTransactions = filteredTransactions.Where(t => t.BudgetType != _budgetTypes.FirstOrDefault(_ => _.IsAdd).Code).ToList();
        }

        if (filteredTransactions == null || filteredTransactions.Count == 0)
        {
            HasReportData = false;
            return;
        }

        HasReportData = true;

        MainFormatOptions = InitializeMainReportFormat();

        finalData = [.. filteredTransactions.Select(x => new WalletItemTransactionModel
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
        })];

        // MainReportTitle = IsMonthly ? $"{MonthFilter.ToString("MMMM", CultureInfo.InvariantCulture)} {MonthFilter.Year}" : $"{MonthFilter.Year}";
        // var title = new StringBuilder();
        // title.Append($"Income vs Expense for {IsMonthly ? MonthFilter.ToString("MMMM", CultureInfo.InvariantCulture) MonthFilter.Year" : $"{MonthFilter.Year}");

        MainReportData = GetMainReportData(finalData);


        IncomeVsExpenseFormatOptions = InitializeIncomeVsExpenseReportFormat();
        IncomeVsExpenseReportData = GetAnnualIncomeVsExpenseReportData(finalData);



        IncomeVsExpenseTitle = $"{YearFilter.Year} Income vs Expense Report";
    }
}
