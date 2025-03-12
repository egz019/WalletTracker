

namespace WalletTracker.Managers;

public interface IPreDataManager : IManager
{
    Task<List<BudgetSubTypeEntity>> GetBudgetSubTypeList();
    Task<List<BudgetTypeEntity>> GetBudgetTypeList();
    void PreloadData();
}
