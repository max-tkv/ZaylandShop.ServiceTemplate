namespace ZaylandShop.ServiceTemplate.Repositories;

public interface IAppUserRepository : IRepository<Entities.AppUser>
{
    Task<Entities.AppUser> AddAsync(Entities.AppUser newUser);
}