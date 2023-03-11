using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZaylandShop.ServiceTemplate.Entities;
using ZaylandShop.ServiceTemplate.Utils.ApiResponse;

namespace ZaylandShop.ServiceTemplate.Controllers.Api.V1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class TestController : Controller
{
    private readonly IMapper _mapper;

    public TestController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseResult<Contracts.Models.User>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse))]
    [Consumes("application/json")]
    [Produces("application/json")]
    public async Task<ApiResponseResult<Contracts.Models.User>> Test([Required] string data)
    {
        var user = new AppUser();
        var dto = _mapper.Map<Contracts.Models.User>(user);
        return ApiResponse.CreateSuccess(dto);
    }
}