using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<CatalogItemDto> GetByIdAsync(int id);
    Task<GetItemByDataResponse<CatalogBrandDto>?> GetCatalogBrendsAsync();
    Task<GetItemByDataResponse<CatalogTypeDto>> GetCatalogTypesAsync();
    Task<GetItemByDataResponse<CatalogItemDto>> GetByBrandAsync(string brand);
    Task<GetItemByDataResponse<CatalogItemDto>> GetByTypeAsync(string type);
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
}