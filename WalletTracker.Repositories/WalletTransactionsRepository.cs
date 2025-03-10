namespace WalletTracker.Repositories;

public class WalletTransactionsRepository : RepositoryBase, IWalletTransactionsRepository
{
	public WalletTransactionsRepository(IAppDatabase appDatabase) : base(appDatabase)
	{
	}

	public async Task<List<WalletTransactionsDto>> GetWalletTransactionsAsync()
	{
		var transactions = await DB.ToListAsync<WalletTransactionsDto>();
		return [.. transactions.Select(t => new WalletTransactionsDto
		{
			TransactionId = t.TransactionId,
			BudgetType = t.BudgetType,
			BudgetSubType = t.BudgetSubType,
			Amount = t.Amount,
			TransactionDate = t.TransactionDate,
			Description = t.Description
		})];
	}

	public async Task<WalletTransactionsDto> GetWalletTransactionAsync(string transactionId)
	{
		var transaction = await DB.FirstOrDefaultAsync<WalletTransactionsDto>(x => x.TransactionId == transactionId);
		return new WalletTransactionsDto
		{
			TransactionId = transaction.TransactionId,
			BudgetType = transaction.BudgetType,
			BudgetSubType = transaction.BudgetSubType,
			Amount = transaction.Amount,
			TransactionDate = transaction.TransactionDate,
			Description = transaction.Description
		};
	}

    public async Task<int> SaveWalletTransactionAsync(WalletTransactionsDto walletTransaction)
    {
        return await DB.InsertAsync(walletTransaction);
    }

	public async Task<int> DeleteWalletTransactionAsync(string transactionId)
	{
		var transaction = await DB.FirstOrDefaultAsync<WalletTransactionsDto>(x => x.TransactionId == transactionId);

        if (transaction == null)
		{
            return 0;
			
		}

        return await DB.DeleteAsync<WalletTransactionsDto>(transaction.Id);
    }
}
