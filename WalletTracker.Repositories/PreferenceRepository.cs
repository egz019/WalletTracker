using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletTracker.Repositories;

public class PreferenceRepository : RepositoryBase, IPreferenceRespository
{
    public PreferenceRepository(IAppDatabase db) : base(db)
    {
    }
    public async Task<PreferenceDto> GetPreferenceAsync(string sharedName)
    {
        return await DB.FirstOrDefaultAsync<PreferenceDto>(x => x.SharedName == sharedName);
    }

    public async Task SavePreferences(PreferenceDto preferences)
    {
        await DB.InsertAsync(preferences);
    }
}
