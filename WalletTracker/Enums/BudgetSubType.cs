using System.ComponentModel;

namespace WalletTracker.Enums;

public enum BudgetSubType
{
    [Description("Salary")]
    Salary,

    [Description("Savings")]
    Savings,

    [Description("Utilities")]
    Utilities,

    [Description("Transportation")]
    Transportation,

    [Description("Food")]
    Food,

    [Description("Shopping")]
    Shopping,

    [Description("Miscellaneous")]
    Miscellaneous,

    [Description("Health")]
    Health,

    [Description("Mortgage")]
    Mortgage,
}

//public static class BudgetSubTypeExtensions
//{
//    public static BudgetSubTypeModel ToBudgetSubTypeModel(this BudgetSubType budgetSubType)
//    {
//        return budgetSubType switch
//        {
//            BudgetSubType.Salary => new BudgetSubTypeModel
//            {
//                BudgetSubType = budgetSubType,
//                Description = "Salary",
//                Icon = "ic_income"
//            },
//            BudgetSubType.Utilities => new BudgetSubTypeModel
//            {
//                BudgetSubType = budgetSubType,
//                Description = "Utilities",
//                Icon = "ic_utilities"
//            },
//            BudgetSubType.Transportation => new BudgetSubTypeModel
//            {
//                BudgetSubType = budgetSubType,
//                Description = "Transportation",
//                Icon = "ic_transportation"
//            },
//            BudgetSubType.Food => new BudgetSubTypeModel
//            {
//                BudgetSubType = budgetSubType,
//                Description = "Food",
//                Icon = "ic_food"
//            },
//            BudgetSubType.Shopping => new BudgetSubTypeModel
//            {
//                BudgetSubType = budgetSubType,
//                Description = "Shopping",
//                Icon = "ic_shopping"
//            },
//            BudgetSubType.Miscellaneous => new BudgetSubTypeModel
//            {
//                BudgetSubType = budgetSubType,
//                Description = "Miscellaneous",
//                Icon = "ic_iscellaneous"
//            },
//            _ => new BudgetSubTypeModel
//            {
//                BudgetSubType = budgetSubType,
//                Description = "Not Defined",
//                Icon = ""
//            },
//        };
//    }
//}
