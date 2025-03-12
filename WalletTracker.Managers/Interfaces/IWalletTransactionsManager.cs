namespace WalletTracker.Managers.Interfaces;

public interface IWalletTransactionsManager : IManager
{
    Task<bool> DeleteWalletTransactionAsync(string transactionId);
    Task<List<WalletTransactionsEntity>> GetListOfWalletTransactionsAsync();
    Task<WalletTransactionsEntity> GetWalletTransactionAsync(string transactionId);
    Task<bool> SaveWalletTransactionAsync(WalletTransactionsEntity walletTransaction);
    void SaveWalletTransactions(List<WalletTransactionsEntity> walletTransactions);
}
