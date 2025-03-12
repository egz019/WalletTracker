namespace WalletTracker.Managers.Interfaces;

public interface IPreferenceManager : IManager
{
    T Get<T>(string key, T defaultValue, string sharedName = null);
    void Set<T>(string key, T value, string sharedName = null);
}
