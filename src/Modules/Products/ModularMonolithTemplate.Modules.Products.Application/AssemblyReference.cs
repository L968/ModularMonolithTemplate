using System.Reflection;

namespace ModularMonolithTemplate.Modules.Products.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
