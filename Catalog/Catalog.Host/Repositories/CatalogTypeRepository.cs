﻿using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogTypeRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogTypeRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(string type)
        {
            var item = await _dbContext.AddAsync(new CatalogType
            {
                Type = type
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<PaginatedItems<CatalogType>> GetTypesByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogTypes
                .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogTypes
                .OrderBy(c => c.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogType>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public async Task<bool> Update(int id, string type)
        {
            var catalogItem = await _dbContext.CatalogTypes
            .FirstOrDefaultAsync(x => x.Id == id);

            if (catalogItem != null)
            {
                catalogItem.Type = type;

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _dbContext.CatalogTypes
               .FirstOrDefaultAsync(s => s.Id == id);

            if (item != null)
            {
                _dbContext.Remove<CatalogType>(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                _logger.LogWarning("Type not found");
                return false;
            }
        }

        public async Task<ListOfItems<CatalogItem>> GetByTypesAsync(string type)
        {
            var totalItems = await _dbContext.CatalogItems
                .LongCountAsync();

            var item = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .OrderBy(c => c.Name)
                .Select(s => s)
                .Where(s => s.CatalogType.Type == type)
                .ToListAsync();

            return new ListOfItems<CatalogItem> { Data = item };
        }
    }
}
