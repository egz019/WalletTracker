
namespace WalletTracker.Repositories.Interfaces;

public interface IWalletTransactionsRepository : IRepository
{
    Task<int> DeleteWalletTransactionAsync(string transactionId);
    Task<WalletTransactionsDto> GetWalletTransactionAsync(string transactionId);
    Task<List<WalletTransactionsDto>> GetWalletTransactionsAsync();
    Task<int> SaveWalletTransactionAsync(WalletTransactionsDto walletTransaction);
    void SaveWalletTransactions(List<WalletTransactionsDto> walletTransactions);
    Task<int> UpdateWalletTransactionAsync(WalletTransactionsDto walletTransaction);
}
