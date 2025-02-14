using Microsoft.Extensions.Logging;
using ModularMonolithTemplate.Common.Application.Abstractions;
using ModularMonolithTemplate.Common.Domain.Exceptions;
using ModularMonolithTemplate.Modules.Products.Application.Products.Commands.DeleteProduct;
using ModularMonolithTemplate.Modules.Products.Domain.Products;
using Moq;

namespace ModularMonolithTemplate.Modules.Products.UnitTests.Products.Commands;

public class DeleteProductTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<DeleteProductHandler>> _loggerMock;
    private readonly DeleteProductHandler _handler;

    public DeleteProductTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<DeleteProductHandler>>();

        _handler = new DeleteProductHandler(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task WhenProductExists_ShouldDeleteProduct()
    {
        // Arrange
        var existingProduct = new Product(
            name: "Product to Delete",
            price: 100m
        );

        var command = new DeleteProductCommand(Id: existingProduct.Id);

        _repositoryMock.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(existingProduct);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(x => x.Delete(existingProduct), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task WhenProductDoesNotExist_ShouldThrowAppException()
    {
        // Arrange
        var command = new DeleteProductCommand(Id: Guid.Empty);

        _repositoryMock.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Product?)null);

        // Act & Assert
        AppException exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal($"No Product found with Id {command.Id}", exception.Message);

        _repositoryMock.Verify(x => x.Delete(It.IsAny<Product>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
