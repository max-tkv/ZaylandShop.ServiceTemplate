namespace ZaylandShop.ServiceTemplate;

public static class ServiceLocator
{
    private static IServiceProvider _provider;

    public static void SetProvider(IServiceProvider provider)
    {
        _provider = provider;
    }

    public static T GetService<T>()
    {
        return (T)_provider.GetService(typeof(T));
    }
}