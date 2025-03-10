namespace WalletTracker.Managers.Interfaces;

public interface IManagerToolkit : IManager
{
    TDestination Map<TDestination>(object value);
    TDestination Map<TSource, TDestination>(TSource value);
}
