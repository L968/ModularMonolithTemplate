namespace ModularMonolithTemplate.Modules.Orders.Application.Features.DeleteProduct;

internal sealed class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
    }
}
