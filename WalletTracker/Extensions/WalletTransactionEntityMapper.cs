using System;
using WalletTracker.Managers;

namespace WalletTracker.Extensions;

public static class WalletTransactionEntityMapper
{
    public static WalletItemTransactionModel FromEntityToModel(this WalletTransactionsEntity entity, IPreDataManager preDataManager)
    {
        if (entity == null) return null;

        return new WalletItemTransactionModel
        {
            TransactionId = entity.TransactionId,
            BudgetType = new BudgetTypeModel()
            {
                Code = entity.BudgetType,
                Description = preDataManager.BudgetTypes.FirstOrDefault(_ => _.Code == entity.BudgetType).Description,
                IsAdd = preDataManager.BudgetTypes.FirstOrDefault(_ => _.Code == entity.BudgetType).IsAdd
            },
            BudgetSubType = new BudgetSubTypeModel()
            {
                Code = entity.BudgetSubType,
                BudgetType = entity.BudgetType,
                Description = preDataManager.BudgetSubTypes.FirstOrDefault(_ => _.Code == entity.BudgetSubType).Description,
                Icon = preDataManager.BudgetSubTypes.FirstOrDefault(_ => _.Code == entity.BudgetSubType).Icon,
            },
            Amount = entity.Amount,
            Description = entity.Description,
            TransactionDate = entity.TransactionDate,
        };
    }
}
