namespace WalletTracker.Models;

public class BudgetTypeModel
{
    public string Code {get; set;}
    public string Description {get; set;}
    public bool IsAdd {get; set;}

    public Color BudgetColor => Description == "Income" ? Color.FromArgb("#CCFFCC") : Color.FromArgb("#FFB89E");
}