namespace WalletTracker.Repositories.Interfaces;

public interface IBudgetSubTypeRepository : IRepository
{
    Task<BudgetSubTypeDto> GetBudgetSubTypeAsync(string code);
    Task<List<BudgetSubTypeDto>> GetBudgetSubTypesListAsync();
    Task SaveBudgetSubTypes(List<BudgetSubTypeDto> budgetSubTypes);
}
