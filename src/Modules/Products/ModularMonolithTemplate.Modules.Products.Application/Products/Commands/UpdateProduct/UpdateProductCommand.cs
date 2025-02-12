using System.Text.Json.Serialization;

namespace ModularMonolithTemplate.Modules.Products.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}
