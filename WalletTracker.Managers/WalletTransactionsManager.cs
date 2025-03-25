using WalletTracker.DataObjects;
using WalletTracker.Repositories.Interfaces;

namespace WalletTracker.Managers;

public class WalletTransactionsManager : ManagerBase, IWalletTransactionsManager
{
    private readonly IWalletTransactionsRepository _walletTransactionsRepository;

    public WalletTransactionsManager(
        IManagerToolkit managerToolkit,
        IWalletTransactionsRepository walletTransactionsRepository)
        : base(managerToolkit)
    {
        _walletTransactionsRepository = walletTransactionsRepository;
    }

    public EventHandler WalletTransactionListChanged {get; set;}

    public async Task<WalletTransactionsEntity> GetWalletTransactionAsync(string transactionId)
    {
        var transactions = await _walletTransactionsRepository.GetWalletTransactionAsync(transactionId);
        return ManagerToolkit.Map<WalletTransactionsEntity>(transactions);
    }

    public async Task<List<WalletTransactionsEntity>> GetListOfWalletTransactionsAsync()
    {
        var transactions = await _walletTransactionsRepository.GetWalletTransactionsAsync();

        if (transactions.Any())
        {
            return ManagerToolkit.Map<List<WalletTransactionsEntity>>(transactions);
        }

        return new List<WalletTransactionsEntity>();
    }

    public async Task<bool> SaveWalletTransactionAsync(WalletTransactionsEntity walletTransaction)
    {
        var result = Convert.ToBoolean(await _walletTransactionsRepository.SaveWalletTransactionAsync(ManagerToolkit.Map<WalletTransactionsDto>(walletTransaction)));
        WalletTransactionListChanged?.Invoke(this, EventArgs.Empty);
        return result;
    }

    public void SaveWalletTransactions(List<WalletTransactionsEntity> walletTransactions)
    {
        _walletTransactionsRepository.SaveWalletTransactions(ManagerToolkit.Map<List<WalletTransactionsDto>>(walletTransactions));
        WalletTransactionListChanged?.Invoke(this, EventArgs.Empty);
    }

    public async Task<bool> DeleteWalletTransactionAsync(string transactionId)
    {
        return Convert.ToBoolean(await _walletTransactionsRepository.DeleteWalletTransactionAsync(transactionId));
    }
}
