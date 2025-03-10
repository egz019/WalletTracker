namespace WalletTracker.Managers
{
    public class ManagerBase
    {
        protected IManagerToolkit ManagerToolkit { get; }

        public ManagerBase(IManagerToolkit managerToolkit)
        {
            ManagerToolkit = managerToolkit;
        }
    }
}
