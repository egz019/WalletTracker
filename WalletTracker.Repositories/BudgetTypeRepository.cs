
namespace WalletTracker.Repositories;

public class BudgetTypeRepository : RepositoryBase, IBudgetTypeRepository
{
    public BudgetTypeRepository(IAppDatabase db) : base(db)
    {
    }

    public async Task<BudgetTypesDto> GetBudgetTypeAsync(string code)
    {
        return await DB.FirstOrDefaultAsync<BudgetTypesDto>(x => x.Code == code);
    }
    public async Task<List<BudgetTypesDto>> GetBudgetTypesListAsync()
    {
        return await DB.ToListAsync<BudgetTypesDto>();
    }
    public async Task SaveBudgetTypes(List<BudgetTypesDto> budgetTypes)
    {
        await DB.InsertAll(budgetTypes);
    }
}
