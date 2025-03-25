namespace WalletTracker.Repositories.Interfaces;

public interface IAppDatabase : IMobileDatabase
{
    Task InitializeTables();
}
