namespace ModularMonolithTemplate.Common.Domain;

public interface IAuditableEntity
{
    DateTime CreatedAtUtc { get; set; }
    DateTime UpdatedAtUtc { get; set; }
}
