using WalletTracker.DataObjects.Base;

namespace WalletTracker.DataObjects;

[Table("BudgetSubType")]
public class BudgetSubTypeDto : DataObjectBase, IBudgetSubTypeDto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public string BudgetType { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
