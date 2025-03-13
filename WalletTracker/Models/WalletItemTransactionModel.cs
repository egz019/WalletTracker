namespace WalletTracker.Models;

public class WalletItemTransactionModel
{
    public string TransactionId { get; set; }
    public BudgetTypeModel BudgetType { get; set; }
    public BudgetSubTypeModel BudgetSubType { get; set;}
    //=> BudgetSubTypeModel.BudgetSubType;
    //public BudgetSubTypeModel BudgetSubTypeModel { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }

    
}
