using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
        }

        if (typeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogTypeId == typeFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.OrderBy(c => c.Name)
           .Include(i => i.CatalogBrand)
           .Include(i => i.CatalogType)
           .Skip(pageSize * pageIndex)
           .Take(pageSize)
           .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
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

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
        var item1 = new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        };
        var item = await _dbContext.AddAsync(item1);

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<bool> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId)
    {
        var catalogItem = await _dbContext.CatalogItems
            .FirstOrDefaultAsync(x => x.Id == id);

        if (catalogItem != null)
        {
            catalogItem.CatalogBrandId = catalogBrandId;
            catalogItem.CatalogTypeId = catalogTypeId;
            catalogItem.Description = description;
            catalogItem.Name = name;
            catalogItem.Price = price;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> Delete(int id)
    {
        var item = await _dbContext.CatalogItems
            .FirstOrDefaultAsync(s => s.Id == id);

        if (item != null)
        {
            _dbContext.Remove<CatalogItem>(item);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            _logger.LogWarning("Item not found");
            return false;
        }
    }

    public async Task<ListOfItems<CatalogItem>> GetByIdAsync(int id)
    {
        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var item = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .OrderBy(c => c.Name)
            .Select(s => s)
            .Where(s => s.Id == id)
            .ToListAsync();

        return new ListOfItems<CatalogItem> { Data = item };
    }
}