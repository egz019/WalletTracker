using WalletTracker.Repositories.Base;

namespace WalletTracker.Repositories;

public class BudgetSubTypeRepository : RepositoryBase, IBudgetSubTypeRepository
{
    public BudgetSubTypeRepository(IAppDatabase db) : base(db)
    {
    }

    public async Task<BudgetSubTypeDto> GetBudgetSubTypeAsync(string code)
    {
        var budgetType = await DB.FirstOrDefaultAsync<BudgetSubTypeDto>(x => x.Code == code);
        return budgetType;
    }

    public async Task<List<BudgetSubTypeDto>> GetBudgetSubTypesListAsync()
    {
        var budgetTypes = await DB.ToListAsync<BudgetSubTypeDto>();
        return budgetTypes;
    }

    public List<BudgetSubTypeDto> GetBudgetSubTypesList()
    {
        return DB.ToList<BudgetSubTypeDto>();
    }

    public void SaveBudgetSubTypes(List<BudgetSubTypeDto> budgetSubTypes)
    {
        DB.InsertAll(budgetSubTypes);
    }
}
