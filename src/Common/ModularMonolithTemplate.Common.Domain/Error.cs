namespace ModularMonolithTemplate.Common.Domain;

public sealed record Error(
    string Message,
    ErrorType ErrorType
);
