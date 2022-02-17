using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _catalogBrandRepository.Object, _catalogTypeRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedBrandSuccess = new PaginatedItems<CatalogBrand>()
        {
            Data = new List<CatalogBrand>()
            {
                new CatalogBrand()
                {
                    Brand = "TestBrand",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogBrandSuccess = new CatalogBrand()
        {
            Brand = "TestBrand"
        };

        var catalogBrandDtoSuccess = new CatalogBrandDto()
        {
            Brand = "TestBrand",
        };

        _catalogBrandRepository.Setup(s => s.GetBrendsByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedBrandSuccess);

        _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogBrandSuccess)))).Returns(catalogBrandDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogBrendsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogBrandAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogBrandRepository.Setup(s => s.GetBrendsByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogBrandDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogBrendsAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedTypeSuccess = new PaginatedItems<CatalogType>()
        {
            Data = new List<CatalogType>()
            {
                new CatalogType()
                {
                    Type = "TestType",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogTypeSuccess = new CatalogType()
        {
            Type = "TestType",
        };

        var catalogTypeDtoSuccess = new CatalogTypeDto()
        {
            Type = "TestType",
        };

        _catalogTypeRepository.Setup(s => s.GetTypesByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedTypeSuccess);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogTypeSuccess)))).Returns(catalogTypeDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogTypesAsync(testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogTypesAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogTypeRepository.Setup(s => s.GetTypesByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogTypeDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogTypesAsync(testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]

    public async Task GetByIdAsync_Success()
    {
        // arrange
        var id = 1;

        var data = new ListOfItems<CatalogItem>
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Id = id,
                    Name = "TestName",
                },
            }
        };

        var itemSuccess = new CatalogItemDto()
        {
            Id = id,
            Name = "TestName"
        };

        var itemByIdSuccess = new GetItemByIdResponse<CatalogItemDto>()
        {
            Id = id,
            Data = itemSuccess
        };

        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == id))).ReturnsAsync(data);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<ListOfItems<CatalogItem>>(i => i.Equals(data)))).Returns(itemSuccess);

        // act
        var result = await _catalogService.GetByIdAsync(id);

        // assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(id);
        result?.Name.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_Failed()
    {
        // arrange
        int id = -10;

        var data = new ListOfItems<CatalogItem>
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Id = id,
                    Name = "TestName",
                },
            }
        };

        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == id))).ReturnsAsync(data);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<ListOfItems<CatalogItem>>(i => i.Equals(data)))).Returns((Func<ListOfItems<CatalogItem>>)null!);

        // act
        var result = await _catalogService.GetByIdAsync(id);

        // assert
        result.Should().BeNull();
    }
}