namespace WalletTracker.Managers.Interfaces;

public interface IServiceMapper
{
    TDestination Map<TSource, TDestination>(TSource value);
    TDestination Map<TDestination>(object value);
}
