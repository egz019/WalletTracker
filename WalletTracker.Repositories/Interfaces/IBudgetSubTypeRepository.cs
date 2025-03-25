namespace WalletTracker.Repositories.Interfaces;

public interface IBudgetSubTypeRepository : IRepository
{
    Task<BudgetSubTypeDto> GetBudgetSubTypeAsync(string code);
    Task<List<BudgetSubTypeDto>> GetBudgetSubTypesListAsync();

    List<BudgetSubTypeDto> GetBudgetSubTypesList();
    void SaveBudgetSubTypes(List<BudgetSubTypeDto> budgetSubTypes);
}
