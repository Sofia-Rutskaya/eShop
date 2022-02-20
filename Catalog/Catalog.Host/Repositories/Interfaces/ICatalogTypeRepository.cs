using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(string type);
        Task<PaginatedItems<CatalogType>> GetTypesByPageAsync(int pageIndex, int pageSize);
        Task<bool> Update(int id, string type);
        Task<bool> Delete(int id);
        Task<ListOfItems<CatalogItem>> GetByTypesAsync(string type);
    }
}
