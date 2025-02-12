namespace ModularMonolithTemplate.Modules.Products.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest;
