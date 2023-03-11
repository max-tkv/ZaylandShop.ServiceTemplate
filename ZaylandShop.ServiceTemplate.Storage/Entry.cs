using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZaylandShop.ServiceTemplate.Abstractions;
using ZaylandShop.ServiceTemplate.Repositories;
using ZaylandShop.ServiceTemplate.Storage.Repositories;

namespace ZaylandShop.ServiceTemplate.Storage;

public static class Entry
{
    private static readonly Action<DbContextOptionsBuilder> DefaultOptionsAction = (options) => { };
        
    /// <summary>
    /// Добавления зависимостей для работы с БД
    /// </summary>
    /// <param name="serviceCollection">serviceCollection</param>
    /// <param name="optionsAction">optionsAction</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddSqlStorage(
        this IServiceCollection serviceCollection, 
        Action<DbContextOptionsBuilder> optionsAction)
    {
        serviceCollection.AddDbContext<AppDbContext>(optionsAction ?? DefaultOptionsAction)
            .AddTransient<IUnitOfWork, UnitOfWork>()
            .AddTransient<IAppUserRepository, AppUserRepository>();
        
        return serviceCollection;
    }
}