using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<PaginatedItems<CatalogBrand>> GetBrendsByPageAsync(int pageIndex, int pageSize);
        Task<int?> Add(int id, string brand);
        Task<ListOfItems<CatalogItem>> GetByBrandAsync(string brand);
    }
}
