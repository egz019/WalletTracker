
namespace WalletTracker.Repositories.Database;

public class AppDatabase : MobileDatabase, IAppDatabase
{
    public AppDatabase() : base("de7f666f-de78-43a1-b31c-bat.db3")
    {
        _dbAsyncConnection.CreateTableAsync<PreferenceDto>();
        _dbAsyncConnection.CreateTableAsync<BudgetTypesDto>();
        _dbAsyncConnection.CreateTableAsync<BudgetSubTypeDto>();

        _dbAsyncConnection.CreateTableAsync<WalletTransactionsDto>();
    }
}
