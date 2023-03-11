using ZaylandShop.ServiceTemplate.Repositories;

namespace ZaylandShop.ServiceTemplate.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IAppUserRepository Users { get; }
}