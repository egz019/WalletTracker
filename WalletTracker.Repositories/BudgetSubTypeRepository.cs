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

    public async Task SaveBudgetSubTypes(List<BudgetSubTypeDto> budgetSubTypes)
    {
        await DB.InsertAll(budgetSubTypes);
    }
}
