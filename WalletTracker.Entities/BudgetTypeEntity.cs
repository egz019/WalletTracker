namespace WalletTracker.Entities;

public class BudgetTypeEntity
{
    public string Code { get; set; }

    public string Description { get; set; }

    public bool IsAdd { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
