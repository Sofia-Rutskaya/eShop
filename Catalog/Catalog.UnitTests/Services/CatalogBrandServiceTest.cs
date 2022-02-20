using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services;

public class CatalogBrandServiceTest
{
    private readonly ICatalogBrandService _catalogService;

    private readonly Mock<ICatalogBrandRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogBrand _testItem = new CatalogBrand()
    {
        Id = 10,
        Brand = "JOjaga"
    };

    public CatalogBrandServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogBrandRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Brand);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.Add(
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Brand);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(true);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Brand);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.Update(
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(false);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Brand);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync(true);

        // act
        var result = await _catalogService.Delete(_testItem.Id);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.Delete(
            It.IsAny<int>())).ReturnsAsync(false);

        // act
        var result = await _catalogService.Delete(_testItem.Id);

        // assert
        result.Should().BeFalse();
    }
}