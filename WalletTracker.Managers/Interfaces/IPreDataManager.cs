

namespace WalletTracker.Managers;

public interface IPreDataManager
{
    Task<List<BudgetSubTypeEntity>> GetBudgetSubTypeList();
    Task<List<BudgetTypeEntity>> GetBudgetTypeList();
    Task PreloadData();
}
