namespace WalletTracker.Models;

public class BudgetItemAccountModel
{
    public int Id { get; set; }
    public BudgetType BudgetType { get; set; }
    public BudgetSubType BudgetSubType => BudgetSubTypeModel.BudgetSubType;
    public BudgetSubTypeModel BudgetSubTypeModel { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }

    public Color BudgetColor => BudgetType == BudgetType.Income ? Color.FromArgb("#CCFFCC") : Color.FromArgb("#FFB89E");
}
