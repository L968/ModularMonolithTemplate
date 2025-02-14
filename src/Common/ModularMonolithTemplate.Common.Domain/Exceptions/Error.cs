namespace ModularMonolithTemplate.Common.Domain.Exceptions;

public sealed record Error(
    string Message,
    ErrorType ErrorType
);
