using AutoMapper;
using ZaylandShop.ServiceTemplate.Entities;

namespace ZaylandShop.ServiceTemplate.Controllers.Mappings;

public class TestControllerMappingProfile : Profile
{
    public TestControllerMappingProfile()
    {
        CreateMap<Contracts.Models.User, AppUser>(MemberList.Destination);
        
        CreateMap<AppUser, Contracts.Models.User>(MemberList.Destination);
    }
}