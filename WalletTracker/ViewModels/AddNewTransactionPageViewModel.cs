
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using WalletTracker.Extensions;

namespace WalletTracker.ViewModels;

public partial class AddNewTransactionPageViewModel : PageViewModelBase
{
    private readonly IWalletTransactionsManager _walletTransactionsManager;
    private readonly IPreDataManager _preDataManager;
    private List<BudgetSubTypeModel> _budgetSubTypeList;
    private WalletItemTransactionModel transactionItem;
    private bool _isInitializing = true;

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
        _budgetSubTypeList = [.. budgetSubTypeList.Select(_ => new BudgetSubTypeModel
        {
            Code = _.Code,
            Description = _.Description,
            BudgetType = _.BudgetType
        })];

        BudgetSubTypes = _budgetSubTypeList;
    }

    protected override async Task OnNavigatedToAsync(INavigationParameters parameters)
    {
        await base.OnNavigatedToAsync(parameters);

        if(parameters.TryGetValue<WalletItemTransactionModel>("Transaction", out transactionItem))
        {
            if (transactionItem != null)
            {
                IsEditMode = true;
                SelectedBudgetType = BudgetTypes.FirstOrDefault(x => x.Code == transactionItem.BudgetType.Code);
                SelectedBudgetSubType = BudgetSubTypes.FirstOrDefault(x => x.Code == transactionItem.BudgetSubType.Code);

                TransactionAmount = transactionItem.Amount;
                Description = transactionItem.Description;
                SelectedTransactionDate = transactionItem.TransactionDate;
            }

            _isInitializing = false;
        }
    }

    [ObservableProperty]
    private bool _isEditMode = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BudgetSubTypes))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private BudgetTypeModel _selectedBudgetType;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private BudgetSubTypeModel _selectedBudgetSubType;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private string _description = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private decimal _transactionAmount = 0;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private DateTime _selectedTransactionDate = DateTime.Now;

    [ObservableProperty]
    private List<BudgetTypeModel> _budgetTypes;

    [ObservableProperty]
    private List<BudgetSubTypeModel> _budgetSubTypes;

    // / <summary>
    // / Handles changes to the selected budget type and updates the budget subtypes accordingly.
    // / </summary>
    partial void OnSelectedBudgetTypeChanged(BudgetTypeModel value)
    {
        if (_budgetSubTypeList == null || _isInitializing)
        {
            return;
        }

        if (SelectedBudgetType != null)
        {
            BudgetSubTypes = _budgetSubTypeList
                .Where(subType => subType.BudgetType == SelectedBudgetType.Code)
                .Select(subType => new BudgetSubTypeModel
                {
                    Code = subType.Code,
                    Description = subType.Description
                })
                .ToList();
        }
        else
        {
            BudgetSubTypes = new List<BudgetSubTypeModel>();
        }
    }

    partial void OnSelectedBudgetTypeChanged(BudgetTypeModel oldValue, BudgetTypeModel newValue)
    {
        if(_isInitializing)
        {
            return;
        }

        if (newValue != null && oldValue != newValue)
        {
            BudgetSubTypes = _budgetSubTypeList.Where(subType => subType.BudgetType == newValue.Code).ToList();

            SelectedBudgetSubType = null;
        }
    }

    [RelayCommand]
    private async Task CloseWindow()
    {
        await NavigationService.GoBackAsync();
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task Save()
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    var isSuccess = false;
                    if (IsEditMode)
                    {
                        isSuccess = await _walletTransactionsManager.UpdateWalletTransactionAsync(new WalletTransactionsEntity
                        {
                            TransactionId = transactionItem.TransactionId,
                            BudgetType = SelectedBudgetType.Code,
                            BudgetSubType = SelectedBudgetSubType.Code,
                            Amount = TransactionAmount,
                            Description = Description,
                            TransactionDate = SelectedTransactionDate
                        });
                    }
                    else
                    {
                        isSuccess = await _walletTransactionsManager.SaveWalletTransactionAsync(new WalletTransactionsEntity
                        {
                            TransactionId = Guid.NewGuid().ToString(),
                            BudgetType = SelectedBudgetType.Code,
                            BudgetSubType = SelectedBudgetSubType.Code,
                            Amount = TransactionAmount,
                            Description = Description,
                            TransactionDate = SelectedTransactionDate
                        });
                    }

                    if (isSuccess)
                    {
                        var toast = Toast.Make("Transaction saved.", ToastDuration.Short);
                        await toast.Show();

                        await CloseWindow();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            });
    }

    private bool CanSave() => !string.IsNullOrEmpty(SelectedBudgetType?.Code) &&
              !string.IsNullOrEmpty(SelectedBudgetSubType?.Code) &&
              !string.IsNullOrEmpty(Description) &&
              (TransactionAmount > 0 || !string.IsNullOrEmpty(TransactionAmount.ToString()));

    [RelayCommand]
    public async Task Cancel()
    {
        await CloseWindow();
    }
}