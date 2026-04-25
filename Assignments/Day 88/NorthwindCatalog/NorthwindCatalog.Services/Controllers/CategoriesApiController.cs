using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NorthwindCatalog.Services.Repositories;
using NorthwindCatalog.Services.DTOs;

[ApiController]
[Route("api/categories")]
public class CategoriesApiController : ControllerBase
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoriesApiController(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _repo.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
    }
}