namespace WalletTracker.Repositories;

public class PreferenceRepository : RepositoryBase, IPreferenceRespository
{
    public PreferenceRepository(IAppDatabase db) : base(db)
    {
    }

    public string Get(string keyName, string sharedName = null)
    {
        var item = DB.FirstOrDefault<PreferenceDto>(x => x.KeyName == keyName && sharedName == null);
        return item != null ? item.Value : null;
    }

    public async Task<string> GetAsync(string keyName, string sharedName = null)
    {
        var item = await DB.FirstOrDefaultAsync<PreferenceDto>(x => x.KeyName == keyName && sharedName == null);
        return item != null ? item.Value : null;
    }

    public async Task SetAsync(string keyName, string value, string sharedName = null)
    {
        var item = await DB.FirstOrDefaultAsync<PreferenceDto>(x => x.KeyName == keyName && sharedName == null);

        if (item == null)
        {
            item = new PreferenceDto()
            {
                KeyName = keyName,
                SharedName = sharedName
            };
        }

        item.Value = value;
        await DB.InsertOrUpdateAsync(item);
    }

    public void Set(string keyName, string value, string sharedName = null)
    {
        var item = DB.FirstOrDefault<PreferenceDto>(x => x.KeyName == keyName && sharedName == null);

        if (item == null)
        {
            item = new PreferenceDto()
            {
                KeyName = keyName,
                SharedName = sharedName
            };
        }

        item.Value = value;
        DB.InsertOrUpdate(item);
    }

    public async Task ClearAsync(string sharedName = null)
    {
        if (string.IsNullOrEmpty(sharedName))
        {
            await DB.DeleteAllAsync<PreferenceDto>();
        }

        var items = await DB.ToListAsync<PreferenceDto>(x => x.SharedName == sharedName);

        foreach (var item in items)
        {
            await DB.DeleteAsync<PreferenceDto>(item.Id);
        }
    }
}
