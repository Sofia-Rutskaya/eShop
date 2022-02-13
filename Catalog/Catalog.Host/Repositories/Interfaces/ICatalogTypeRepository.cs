using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(int id, string type);
        Task<PaginatedItems<CatalogType>> GetTypesByPageAsync(int pageIndex, int pageSize);
        Task Update(int id, string type);
        Task Delete(int id, string type);
    }
}
