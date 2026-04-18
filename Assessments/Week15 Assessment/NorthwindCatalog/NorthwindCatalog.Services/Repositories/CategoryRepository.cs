using Microsoft.EntityFrameworkCore;
using NorthwindCatalog.Services.Data;
using NorthwindCatalog.Services.Models;
using NorthwindCatalog.Services.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly NorthwindContext _context;

    public CategoryRepository(NorthwindContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }
}