﻿using System.Reflection;

namespace ModularMonolithTemplate.Modules.Orders.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
