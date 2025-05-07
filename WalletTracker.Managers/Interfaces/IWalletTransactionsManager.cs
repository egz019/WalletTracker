namespace WalletTracker.Managers.Interfaces;

public interface IWalletTransactionsManager : IManager
{
    EventHandler WalletTransactionListChanged {get; set;}
    Task<bool> DeleteWalletTransactionAsync(string transactionId);
    Task<List<WalletTransactionsEntity>> GetListOfWalletTransactionsAsync();
    Task<WalletTransactionsEntity> GetWalletTransactionAsync(string transactionId);
    Task<bool> SaveWalletTransactionAsync(WalletTransactionsEntity walletTransaction);
    void SaveWalletTransactions(List<WalletTransactionsEntity> walletTransactions);
    Task<bool> UpdateWalletTransactionAsync(WalletTransactionsEntity walletTransaction);
}
