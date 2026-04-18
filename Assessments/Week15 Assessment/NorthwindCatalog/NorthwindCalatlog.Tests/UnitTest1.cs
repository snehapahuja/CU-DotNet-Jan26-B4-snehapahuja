using Xunit;
using NorthwindCatalog.Services.DTOs;

public class UnitTest1  
{
    [Fact]
    public void Test1() 
    {
        // Arrange
        var product = new ProductDto
        {
            UnitPrice = 10m,
            UnitsInStock = 5
        };

        // Act
        var result = product.InventoryValue;

        // Assert
        Assert.Equal(50m, result);
    }
}