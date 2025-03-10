namespace WalletTracker.DataObjects.Interfaces;

public interface IWalletTransactionsDto
{
    public string TransactionId { get; set; }
    public string BudgetType { get; set; }
    public string BudgetSubType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
}
