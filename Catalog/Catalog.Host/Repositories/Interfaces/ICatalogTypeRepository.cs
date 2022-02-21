using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(string type);
        Task<ListOfItems<CatalogType>> GetTypesAsync();
        Task<bool> Update(int id, string type);
        Task<bool> Delete(int id);
        Task<ListOfItems<CatalogItem>> GetByTypesAsync(string type);
    }
}
