using WalletTracker.DataObjects.Base;

namespace WalletTracker.DataObjects;

[Table("BudgetTypes")]
public class BudgetTypesDto : DataObjectBase, IBudgetTypesDto
{
    public string Code { get; set; }

    public string Description { get; set; }

    public bool IsAdd { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
