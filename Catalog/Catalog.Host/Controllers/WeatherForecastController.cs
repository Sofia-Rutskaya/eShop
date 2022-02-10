using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Route("[controller]")]
public class OutwearController : ControllerBase
{
    private static readonly string[] Colors = new[]
    {
        "Black", "Pink", "Olive", "Wheat", "White Smoke", "Coffee", "Orange Red",
        "Desert sand", "Slate gray", "Silver", "White", "Dark Sea Green", "Indigo"
    };

    private static readonly string[] InternationalSize = new[]
    {
        "XS", "S", "M", "L", "XL", "XXL", "XXXL"
    };

    private static readonly string[] NameOutwear = new[]
    {
        "Bomber Jacket", "Leather Jacket", "Hoodie", "Quilted Jacket", "Blazer",
        "Cropped Jacket", "Cashmere/Wool Jackets", "Barbour", "Parka", "Coat"
    };

    private readonly ILogger<OutwearController> _logger;

    public OutwearController(ILogger<OutwearController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Outerwear> Get()
    {
        return Enumerable.Range(2, 6).Select(index => new Outerwear
        {
            Name = NameOutwear[Random.Shared.Next(NameOutwear.Length)],
            Size = InternationalSize[Random.Shared.Next(InternationalSize.Length)],
            Price = Random.Shared.Next(1000, 5000),
            Color = Colors[Random.Shared.Next(Colors.Length)]
        })
            .ToArray();
    }
}