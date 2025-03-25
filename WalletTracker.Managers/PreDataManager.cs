using WalletTracker.DataObjects;

namespace WalletTracker.Managers;

public class PreDataManager : ManagerBase, IPreDataManager
{
    private readonly IBudgetSubTypeRepository _budgetSubTypeRepository;
    private readonly IBudgetTypeRepository _budgetTypesRepository;

    public IList<BudgetTypeEntity> BudgetTypes { get; set; }
    public IList<BudgetSubTypeEntity> BudgetSubTypes { get; set; }

    public PreDataManager(
        IManagerToolkit managerToolkit,
        IBudgetTypeRepository budgetTypesRepository,
        IBudgetSubTypeRepository budgetSubTypeRepository) : base(managerToolkit)
    {
        _budgetSubTypeRepository = budgetSubTypeRepository;
        _budgetTypesRepository = budgetTypesRepository;
        
        BudgetTypes = new List<BudgetTypeEntity>();
        BudgetSubTypes = new List<BudgetSubTypeEntity>();
    }

    public void PreloadData()
    {
        PreloadBudgetType();
        PreloadBudgetSubTypes();
    }

    public void InitializeData()
    {
        BudgetTypes = GetBudgetTypeList();
        BudgetSubTypes = GetBudgetSubTypeList();
    }

    public List<BudgetTypeEntity> GetBudgetTypeList()
    {
       var budgetTypes = _budgetTypesRepository.GetBudgetTypesList();

        if (budgetTypes.Any())
        {
            return ManagerToolkit.Map<List<BudgetTypeEntity>>(budgetTypes);
        }

        return new List<BudgetTypeEntity>();
    }

	public List<BudgetSubTypeEntity> GetBudgetSubTypeList()
	{
		var subTypes = _budgetSubTypeRepository.GetBudgetSubTypesList();

        if (subTypes.Any())
        {
            return ManagerToolkit.Map<List<BudgetSubTypeEntity>>(subTypes);
        }
        return new List<BudgetSubTypeEntity>();
    }

    public async Task<List<BudgetTypeEntity>> GetBudgetTypeListAsync()
    {
       var budgetTypes = await _budgetTypesRepository.GetBudgetTypesListAsync();

        if (budgetTypes.Any())
        {
            return ManagerToolkit.Map<List<BudgetTypeEntity>>(budgetTypes);
        }

        return new List<BudgetTypeEntity>();
    }

	public async Task<List<BudgetSubTypeEntity>> GetBudgetSubTypeListAsync()
	{
		var subTypes = await _budgetSubTypeRepository.GetBudgetSubTypesListAsync();

        if (subTypes.Any())
        {
            return ManagerToolkit.Map<List<BudgetSubTypeEntity>>(subTypes);
        }
        return new List<BudgetSubTypeEntity>();
    }

    private void PreloadBudgetType()
    {
        _budgetTypesRepository.SaveBudgetTypes(
        [
            new BudgetTypesDto
            {
                Code = "01",
                Description = "Income",
                IsAdd = true
            },
            new BudgetTypesDto
            {
                Code = "02",
                Description = "Expense",
                IsAdd = false
            }
        ]);
    }

    private void PreloadBudgetSubTypes()
    {
        _budgetSubTypeRepository.SaveBudgetSubTypes(
        [
            new BudgetSubTypeDto
            {
                Code = "BS01",
                Description = "Salary",
                BudgetType = "01",
                Icon = "ic_income.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS02",
                Description = "Savings",
                BudgetType = "01",
                Icon = "ic_savings.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS03",
                Description = "Utilities",
                BudgetType = "02",
                Icon = "ic_utilities.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS04",
                Description = "Transportation",
                BudgetType = "02",
                Icon = "ic_transportation.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS05",
                Description = "Food",
                BudgetType = "02",
                Icon = "ic_food.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS06",
                Description = "Shopping",
                BudgetType = "02",
                Icon = "ic_shopping.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS07",
                Description = "Miscellaneous",
                BudgetType = "02",
                Icon = "ic_miscellaneous.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS08",
                Description = "Health",
                BudgetType = "02",
                Icon = "ic_health.svg"
            },
            new BudgetSubTypeDto
            {
                Code = "BS09",
                Description = "Mortgage",
                BudgetType = "02",
                Icon = "ic_mortgage.svg"
            }
        ]);
    }
}
