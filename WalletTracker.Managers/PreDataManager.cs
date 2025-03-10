using WalletTracker.DataObjects;
using WalletTracker.Repositories.Interfaces;

namespace WalletTracker.Managers;

public class PreDataManager : ManagerBase, IPreDataManager
{
    private readonly IBudgetSubTypeRepository _budgetSubTypeRepository;
    private readonly IBudgetTypeRepository _budgetTypesRepository;
    public PreDataManager(IManagerToolkit managerToolkit, IBudgetTypeRepository budgetTypesRepository, IBudgetSubTypeRepository budgetSubTypeRepository) : base(managerToolkit)
    {
        _budgetSubTypeRepository = budgetSubTypeRepository;
        _budgetTypesRepository = budgetTypesRepository;
    }

    public async Task PreloadData()
    {
        await PreloadBudgetType();
        await PreloadBudgetSubTypes();
    }

    public async Task<List<BudgetTypeEntity>> GetBudgetTypeList()
    {
       var budgetTypes = await _budgetTypesRepository.GetBudgetTypesListAsync();

        if (budgetTypes.Any())
        {
            return ManagerToolkit.Map<List<BudgetTypeEntity>>(budgetTypes);
        }

        return new List<BudgetTypeEntity>();
    }

	public async Task<List<BudgetSubTypeEntity>> GetBudgetSubTypeList()
	{
		var subTypes = await _budgetSubTypeRepository.GetBudgetSubTypesListAsync();

        if (subTypes.Any())
        {
            return ManagerToolkit.Map<List<BudgetSubTypeEntity>>(subTypes);
        }
        return new List<BudgetSubTypeEntity>();
    }

    private async Task PreloadBudgetType()
    {
        await _budgetTypesRepository.SaveBudgetTypes(
        [
            new BudgetTypesDto
            {
                Code = "01",
                Description = "Income"
            },
            new BudgetTypesDto
            {
                Code = "02",
                Description = "Expense"
            }
        ]);
    }

    private async Task PreloadBudgetSubTypes()
    {
        await _budgetSubTypeRepository.SaveBudgetSubTypes(
        [
            new BudgetSubTypeDto
            {
                Code = "BS01",
                Description = "Salary"
            },
            new BudgetSubTypeDto
            {
                Code = "BS02",
                Description = "Savings"
            },
            new BudgetSubTypeDto
            {
                Code = "BS03",
                Description = "Utilities"
            },
            new BudgetSubTypeDto
            {
                Code = "BS04",
                Description = "Transportation"
            },
            new BudgetSubTypeDto
            {
                Code = "BS05",
                Description = "Food"
            },
            new BudgetSubTypeDto
            {
                Code = "BS06",
                Description = "Shopping"
            },
            new BudgetSubTypeDto
            {
                Code = "BS07",
                Description = "Miscellaneous"
            },
            new BudgetSubTypeDto
            {
                Code = "BS08",
                Description = "Health"
            },
            new BudgetSubTypeDto
            {
                Code = "BS09",
                Description = "Mortgage"
            }
        ]);
    }
}
