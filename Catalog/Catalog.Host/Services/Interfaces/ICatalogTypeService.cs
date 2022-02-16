namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Add(string type);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, string type);
    }
}
