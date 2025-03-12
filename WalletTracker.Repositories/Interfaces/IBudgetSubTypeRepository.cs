namespace WalletTracker.Repositories.Interfaces;

public interface IBudgetSubTypeRepository : IRepository
{
    Task<BudgetSubTypeDto> GetBudgetSubTypeAsync(string code);
    Task<List<BudgetSubTypeDto>> GetBudgetSubTypesListAsync();
    void SaveBudgetSubTypes(List<BudgetSubTypeDto> budgetSubTypes);
}
