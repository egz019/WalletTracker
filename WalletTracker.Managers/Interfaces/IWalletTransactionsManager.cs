namespace WalletTracker.Managers.Interfaces;

public interface IWalletTransactionsManager
{
    Task<bool> DeleteWalletTransactionAsync(string transactionId);
    Task<List<WalletTransactionsEntity>> GetListOfWalletTransactionsAsync();
    Task<WalletTransactionsEntity> GetWalletTransactionAsync(string transactionId);
    Task<bool> SaveWalletTransactionAsync(WalletTransactionsEntity walletTransaction);
}
