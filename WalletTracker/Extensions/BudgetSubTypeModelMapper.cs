using System;

namespace WalletTracker.Extensions;

public static class BudgetSubTypeEntityMapper
{
    public static BudgetSubTypeModel FromEntityToModel(this BudgetSubTypeEntity entity)
    {
        return new BudgetSubTypeModel
        {
            Code = entity.Code,
            Description = entity.Description,
            Icon = entity.Icon,
            BudgetType = entity.BudgetType
        };
    }
}
