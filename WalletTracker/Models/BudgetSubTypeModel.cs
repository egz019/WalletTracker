namespace WalletTracker.Models;

public class BudgetSubTypeModel
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string BudgetType {get; set; }
    public string Icon { get; set; }

    // public BudgetSubTypeModel(BudgetSubType budgetSubType)
    // {
    //     BudgetSubType = budgetSubType;

    //     switch (budgetSubType)
    //     {
    //         case BudgetSubType.Salary:
    //             Description = "Salary";
    //             Icon = "ic_income";
    //             break;
    //         case BudgetSubType.Savings:
    //             Description = "Savings";
    //             Icon = "ic_savings";
    //             break;
    //         case BudgetSubType.Utilities:
    //             Description = "Utilities";
    //             Icon = "ic_utilities";
    //             break;
    //         case BudgetSubType.Transportation:
    //             Description = "Transportation";
    //             Icon = "ic_transportation";
    //             break;
    //         case BudgetSubType.Food:
    //             Description = "Food";
    //             Icon = "ic_food";
    //             break;
    //         case BudgetSubType.Shopping:
    //             Description = "Shopping";
    //             Icon = "ic_shopping";
    //             break;
    //         case BudgetSubType.Miscellaneous:
    //             Description = "Miscellaneous";
    //             Icon = "ic_miscellaneous";
    //             break;
    //         case BudgetSubType.Health:
    //             Description = "Health";
    //             Icon = "ic_health";
    //             break;
    //         case BudgetSubType.Mortgage:
    //             Description = "Mortgage";
    //             Icon = "ic_mortgage";
    //             break;
    //         default:
    //             Description = "Not Defined";
    //             Icon = string.Empty;
    //             break;
    //     }
    // }
}
