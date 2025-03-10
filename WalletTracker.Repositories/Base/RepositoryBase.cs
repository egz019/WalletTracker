namespace WalletTracker.Repositories.Base;

public class RepositoryBase
{
    protected IMobileDatabase DB;

    public RepositoryBase(IMobileDatabase db)
    {
        DB = db;
    }
}
