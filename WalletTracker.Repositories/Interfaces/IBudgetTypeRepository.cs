namespace WalletTracker.Repositories.Interfaces;

public  interface IBudgetTypeRepository : IRepository
{
    Task<BudgetTypesDto> GetBudgetTypeAsync(string code);
    Task<List<BudgetTypesDto>> GetBudgetTypesListAsync();

    List<BudgetTypesDto> GetBudgetTypesList();
    void SaveBudgetType(BudgetTypesDto budgetType);
    void SaveBudgetTypes(List<BudgetTypesDto> budgetTypes);
    //void SaveBudgetItemType(BudgetTypesDto budgetType);
    //Task SaveBudgetType(BudgetTypesDto budgetType);
    //Task SaveBudgetTypes(List<BudgetTypesDto> budgetTypes);
}
