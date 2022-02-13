using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<GetItemByIdResponse<CatalogItemDto>> GetByIdAsync(int id);
    Task<PaginatedItemsResponse<CatalogBrandDto>> GetCatalogBrendsAsync(int pageSize, int pageIndex);
    Task<PaginatedItemsResponse<CatalogTypeDto>> GetCatalogTypesAsync(int pageSize, int pageIndex);
    Task<GetItemByDataResponse<CatalogItemDto>> GetByBrandAsync(string brand);
}