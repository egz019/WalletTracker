

namespace WalletTracker.Managers;

public interface IPreDataManager : IManager
{
    public IList<BudgetTypeEntity> BudgetTypes {get; set;}
    public IList<BudgetSubTypeEntity> BudgetSubTypes {get; set;}
    Task<List<BudgetSubTypeEntity>> GetBudgetSubTypeListAsync();
    Task<List<BudgetTypeEntity>> GetBudgetTypeListAsync();
    List<BudgetTypeEntity> GetBudgetTypeList();
    List<BudgetSubTypeEntity> GetBudgetSubTypeList();
    void PreloadData();
    void InitializeData();
}
