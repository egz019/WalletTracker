
namespace WalletTracker.Managers;

public class PreferenceManager : IPreferenceManager
{
    private readonly IPreferenceRespository _preferenceRepository;

    public PreferenceManager(IPreferenceRespository preferenceRespository)
    {
        _preferenceRepository = preferenceRespository;
    }

    public T Get<T>(string key, T defaultValue, string? sharedName = null)
    {
        var data = _preferenceRepository.Get(key, sharedName);

        if (!string.IsNullOrEmpty(data))
        {
            return JsonSerializer.Deserialize<T>(data) ?? defaultValue;
        }

        return defaultValue;
    }

    public void Set<T>(string key, T value, string? sharedName = null)
    {
        var data = JsonSerializer.Serialize<T>(value);
        _preferenceRepository.Set(key, data, sharedName);
    }
}
