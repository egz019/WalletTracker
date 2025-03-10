using WalletTracker.DataObjects.Base;

namespace WalletTracker.DataObjects;

[Table("Preference")]
public class PreferenceDto : DataObjectBase
{
    public string SharedName { get; set; }

    public string KeyName { get; set; }

    public string Value { get; set; }
}
