namespace WalletTracker.DataObjects.Interfaces;

public interface IDataObjectBase
{
    int Id { get; set; }
    DateTime Created { get; set; }
    DateTime Modified { get; set; }
}
