namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<bool> Delete(int id);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<bool> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId);
}