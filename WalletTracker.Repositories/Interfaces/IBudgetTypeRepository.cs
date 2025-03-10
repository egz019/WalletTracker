namespace WalletTracker.Repositories.Interfaces;

public  interface IBudgetTypeRepository : IRepository
{
    Task<BudgetTypesDto> GetBudgetTypeAsync(string code);
    Task<List<BudgetTypesDto>> GetBudgetTypesListAsync();
    Task SaveBudgetTypes(List<BudgetTypesDto> budgetTypes);
}
