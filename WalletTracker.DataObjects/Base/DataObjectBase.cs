namespace WalletTracker.DataObjects.Base;

public class DataObjectBase : IDataObjectBase
{
    [PrimaryKey, AutoIncrement]
    public virtual int Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
