using Microsoft.Extensions.Logging;
using ModularMonolithTemplate.Modules.Products.Application.Abstractions;
using ModularMonolithTemplate.Modules.Products.Application.Products.Commands.CreateProduct;
using ModularMonolithTemplate.Modules.Products.Domain.Products;
using Moq;

namespace ModularMonolithTemplate.Modules.Products.UnitTests.Application.Products.Commands;

public class CreateProductTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateProductHandler _handler;

    public CreateProductTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<CreateProductHandler>>();

        _handler = new CreateProductHandler(_repositoryMock.Object, _unitOfWorkMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task WhenProductDoesNotExist_ShouldCreateNewProduct()
    {
        // Arrange
        var command = new CreateProductCommand(
            Name: "New Product",
            Price: 150m
        );

        // Act
        CreateProductResponse result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Product", result.Name);
        Assert.Equal(150m, result.Price);
        _repositoryMock.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
