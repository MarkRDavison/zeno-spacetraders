namespace mark.davison.common.client.desktop.source.generators;

[ExcludeFromCodeCoverage]
public static class SourceGeneratorHelpers
{
    public static string GetNamespace(ITypeSymbol syntax)
    {
        string nameSpace = string.Empty;

        INamespaceSymbol? namespaceSymbol = syntax.ContainingNamespace;

        while (!string.IsNullOrEmpty(namespaceSymbol?.Name))
        {
            nameSpace = namespaceSymbol!.Name + (string.IsNullOrEmpty(nameSpace) ? "" : ".") + nameSpace;
            namespaceSymbol = namespaceSymbol.ContainingNamespace;
        }

        return nameSpace;
    }

    public static string GetFullyQualifiedName(ITypeSymbol symbol)
    {
        return $"{GetNamespace(symbol)}.{symbol.Name}";
    }

    public static IEnumerable<ITypeSymbol> GetAllTypes(INamespaceSymbol root)
    {
        foreach (var namespaceOrTypeSymbol in root.GetMembers())
        {
            if (namespaceOrTypeSymbol is INamespaceSymbol ns)
            {
                foreach (var nested in GetAllTypes(ns))
                {
                    yield return nested;
                }
            }

            else if (namespaceOrTypeSymbol is ITypeSymbol type)
            {
                yield return type;
            }
        }
    }

    // TODO: Backport to common source gen helpers
    public static List<ITypeSymbol> GetPotentialTypeSymbols(GeneratorExecutionContext context, HashSet<string> namespaces)
    {
        var rootSymbols = context.Compilation
            .GetSymbolsWithName(_ => true)
            .SelectMany(_ =>
            {
                if (_ is INamespaceSymbol ns)
                {
                    return GetAllTypes(ns);
                }

                else if (_ is ITypeSymbol type)
                {
                    return [type];
                }

                return [];
            });

        var referencedSymbols = context.Compilation.SourceModule.ReferencedAssemblySymbols
            .Where(_ => namespaces.Any(__ => _.Identity.Name.StartsWith(__)))
            .SelectMany(_ =>
            {
                try
                {
                    var main = _.Identity.Name.Split('.').Aggregate(_.GlobalNamespace, (s, c) => s.GetNamespaceMembers().Single(m => m.Name.Equals(c)));

                    return GetAllTypes(main);
                }
                catch
                {
                    return [];
                }
            });

        List<ITypeSymbol> allSymbols = [];

        foreach (var symbol in rootSymbols.Concat(referencedSymbols))
        {
            var ns = GetNamespace(symbol);

            if (namespaces.Any(ns.StartsWith))
            {
                allSymbols.Add(symbol);
            }
        }

        return allSymbols;
    }
}
