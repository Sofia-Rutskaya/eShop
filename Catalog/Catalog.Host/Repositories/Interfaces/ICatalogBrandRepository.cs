using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<ListOfItems<CatalogBrand>> GetBrendsAsync();
        Task<int?> Add(string brand);
        Task<ListOfItems<CatalogItem>> GetByBrandAsync(string brand);
        Task<bool> Update(int id, string brand);
        Task<bool> Delete(int id);
    }
}
