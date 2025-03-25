namespace WalletTracker.ViewModels;

public partial class AddNewTransactionPageViewModel : PageViewModelBase
{
    private readonly IWalletTransactionsManager _walletTransactionsManager;
    private readonly IPreDataManager _preDataManager;

    private List<BudgetSubTypeModel> _budgetSubTypeList;

    public AddNewTransactionPageViewModel(
        BaseServices baseServices, 
        IWalletTransactionsManager walletTransactionsManager, 
        IPreDataManager preDataManager) 
    : base(baseServices)
	{
        _walletTransactionsManager = walletTransactionsManager;
        _preDataManager = preDataManager;
    }

    protected override async Task InitializeAsync(INavigationParameters parameters)
    {
        await base.InitializeAsync(parameters);

        var budgetTypeList = _preDataManager.BudgetTypes;
        BudgetTypes = [.. budgetTypeList.Select(_ => new BudgetTypeModel
        {
            Code = _.Code,
            Description = _.Description
        })];

        var budgetSubTypeList = _preDataManager.BudgetSubTypes;
        BudgetSubTypes = [.. budgetSubTypeList.Select(_ => new BudgetSubTypeModel
        {
            Code = _.Code,
            Description = _.Description,
            BudgetType = _.BudgetType
        })];
        _budgetSubTypeList = BudgetSubTypes;

        SelectedTransactionDate = DateTime.Now;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BudgetSubTypes))]
    //[NotifyPropertyChangedFor(nameof(BudgetSubTypes))]
    private BudgetTypeModel _selectedBudgetType;

    [ObservableProperty]
    private BudgetSubTypeModel _selectedBudgetSubType;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private decimal _transactionAmount;

    [ObservableProperty]
    private DateTime _selectedTransactionDate;

    [ObservableProperty]
    private List<BudgetTypeModel> _budgetTypes;

    [ObservableProperty]
    private List<BudgetSubTypeModel> _budgetSubTypes;

    /// <summary>
    /// Handles changes to the selected budget type and updates the budget subtypes accordingly.
    /// </summary>
    // partial void OnSelectedBudgetTypeChanged(BudgetTypeModel value)
    // {
    //     if (SelectedBudgetType != null)
    //     {
    //         BudgetSubTypes = _preDataManager.GetBudgetSubTypeList()
    //             .Result
    //             .Where(subType => subType.BudgetType == SelectedBudgetType.Code)
    //             .Select(subType => new BudgetSubTypeModel
    //             {
    //                 Code = subType.Code,
    //                 Description = subType.Description
    //             })
    //             .ToList();
    //     }
    //     else
    //     {
    //         BudgetSubTypes = new List<BudgetSubTypeModel>();
    //     }
    // }

    partial void OnSelectedBudgetTypeChanged(BudgetTypeModel oldValue, BudgetTypeModel newValue)
    {
        if (newValue != null && oldValue != newValue)
        {
            BudgetSubTypes = _budgetSubTypeList
            .Where(subType => subType.BudgetType == newValue.Code).ToList();

            SelectedBudgetSubType = null;
        }
    }

    [RelayCommand]
    public async Task CloseWindow()
    {
        await NavigationService.GoBackAsync();
    }

    [RelayCommand]
    public async Task Save()
    {
        try 
        {
            var isSuccess = await _walletTransactionsManager.SaveWalletTransactionAsync(new WalletTransactionsEntity
            {
                TransactionId = Guid.NewGuid().ToString(),
                BudgetType = SelectedBudgetType.Code,
                BudgetSubType = SelectedBudgetSubType.Code,
                Amount = TransactionAmount,
                Description = Description,
                TransactionDate = SelectedTransactionDate
            });

            if(isSuccess)
            {
                await CloseWindow();
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

    [RelayCommand]
    public async Task Cancel()
    {
        await CloseWindow();
    }
}