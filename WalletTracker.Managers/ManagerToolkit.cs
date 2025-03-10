namespace WalletTracker.Managers;

public class ManagerToolkit : IManagerToolkit
{
    private readonly IServiceMapper _mapper;

    public ManagerToolkit(IServiceMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object value) => _mapper.Map<TDestination>(value);
    public TDestination Map<TSource, TDestination>(TSource value) => _mapper.Map<TSource, TDestination>(value);
}
