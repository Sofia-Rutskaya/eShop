namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Add(string brand);
        Task Delete(int id, string brand);
        Task Update(int id, string brand);
    }
}
