namespace WalletTracker.Entities;

public interface ILookupData
{
    IEnumerable<T> GetAll<T>() where T : BaseData;

    T GetByCode<T>(string code) where T : BaseData;
}