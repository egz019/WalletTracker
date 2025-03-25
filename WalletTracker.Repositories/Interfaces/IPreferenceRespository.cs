namespace WalletTracker.Repositories.Interfaces;

public interface IPreferenceRespository : IRepository
{
    Task ClearAsync(string? sharedName = null);
    string Get(string keyName, string? sharedName = null);
    Task<string> GetAsync(string keyName, string? sharedName = null);
    void Set(string keyName, string value, string? sharedName = null);
    Task SetAsync(string keyName, string value, string? sharedName = null);
}
