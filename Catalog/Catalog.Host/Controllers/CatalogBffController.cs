using Catalog.Host.Configurations;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly IOptions<CatalogConfig> _config;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        IOptions<CatalogConfig> config)
    {
        _logger = logger;
        _catalogService = catalogService;
        _config = config;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByDataResponse<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrends()
    {
        var result = await _catalogService.GetCatalogBrendsAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByDataResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes()
    {
        var result = await _catalogService.GetCatalogTypesAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByIdResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(GetItemByIdRequest request)
    {
        var result = await _catalogService.GetByIdAsync(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByDataResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByBrend(GetByDataRequest request)
    {
        var result = await _catalogService.GetByBrandAsync(request.Data);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GetItemByDataResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByType(GetByDataRequest request)
    {
        var result = await _catalogService.GetByTypeAsync(request.Data);
        return Ok(result);
    }
}