using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogBrandRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogBrand>> GetBrendsByPageAsync(int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogBrands
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogBrands
            .OrderBy(c => c.Id)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogBrand>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> Add(string brand)
    {
        var item = await _dbContext.AddAsync(new CatalogBrand
        {
            Brand = brand
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<ListOfItems<CatalogItem>> GetByBrandAsync(string brand)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var item = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Select(s => s)
            .Where(s => s.CatalogBrand.Brand == brand)
            .ToListAsync();

        return new ListOfItems<CatalogItem> { Data = item };
    }

    public async Task Update(int id, string brand)
    {
        var catalogItem = new CatalogBrand
        {
            Id = id,
            Brand = brand
        };
        _dbContext.Update<CatalogBrand>(catalogItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id, string brand)
    {
        var item = await _dbContext.CatalogBrands
            .Select(s => s)
            .Where(s => s.Id == id && s.Brand == brand)
            .FirstOrDefaultAsync();

        if (item != null)
        {
            _dbContext.Remove<CatalogBrand>(item);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            _logger.LogWarning("Type not found");
        }
    }
}