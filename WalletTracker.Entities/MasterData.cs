
using System.Reflection;

namespace WalletTracker.Entities;

public class MasterData : ILookupData
{
    public T GetByCode<T>(string code) where T : BaseData
    {
        //TODO Create custom exception in Common Project
        return GetAll<T>().FirstOrDefault(x => x.Code == code) ?? throw new Exception($"No data found for code {code}");
    }

    public IEnumerable<T> GetAll<T>() where T : BaseData
    {
        var properties = from prop in typeof(MasterData).GetRuntimeProperties()
                         where prop.PropertyType == typeof(IEnumerable<T>)
                         select prop;

        var masterDataLookup = properties.FirstOrDefault(_ => _.PropertyType.GenericTypeArguments[0] == typeof(T));

        if(properties != null && masterDataLookup?.GetValue(this) is IEnumerable<T> masterDataList)
        {
            return masterDataList;
        }
        else
        {
            return new List<T>();
        }
    }
}