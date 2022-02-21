using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.Models.Requests;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog?> GetCatalogItems(int page, int take, int? brand, int? type)
    {
        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (brand.HasValue)
        {
            filters.Add(CatalogTypeFilter.Brand, brand.Value);
        }

        if (type.HasValue)
        {
            filters.Add(CatalogTypeFilter.Type, type.Value);
        }

        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter>>(
           $"{_settings.Value.CatalogUrl}/items",
           HttpMethod.Post,
           new PaginatedItemsRequest<CatalogTypeFilter>()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            });

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        var url = @$"http://www.alevelwebsite.com:5000/api/v1/CatalogBff/GetBrends";

        var result = await _httpClient.SendAsync<DataItemRequest<CatalogBrand>, DataItemRequest<CatalogBrand>>(
           url,
           HttpMethod.Post,
           null !);

        var list = new List<SelectListItem>();
        if (result != null)
        {
            foreach (var item in result.Items)
            {
                list.Add(new SelectListItem()
                {
                    Value = $"{item.Id}",
                    Text = item.Brand
                });
            }
        }

        return list;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        var url = @$"http://www.alevelwebsite.com:5000/api/v1/CatalogBff/GetTypes";

        var result = await _httpClient.SendAsync<DataItemRequest<CatalogType>, DataItemRequest<CatalogType>>(
           url,
           HttpMethod.Post,
           null!);

        var list = new List<SelectListItem>();
        if (result != null)
        {
            foreach (var item in result.Items)
            {
                list.Add(new SelectListItem()
                {
                    Value = $"{item.Id}",
                    Text = item.Type
                });
            }
        }

        return list;
    }
}
