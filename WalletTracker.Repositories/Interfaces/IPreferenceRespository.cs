namespace WalletTracker.Repositories.Interfaces;

public interface IPreferenceRespository : IRepository
{
    public Task<PreferenceDto> GetPreferenceAsync(string sharedName);
    public Task SavePreferences(PreferenceDto preferences);
}
